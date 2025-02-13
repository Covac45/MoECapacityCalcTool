using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;

namespace MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{

    public interface IExitCapacityStructsService
    {
        public List<ExitCapacityStruct> GetExitCapacityStructsForNonStairExits(List<Exit> storeyExits, List<Exit> finalExits);
        public Dictionary<Stair, List<ExitCapacityStruct>> GetExitCapacityStructsForStairExits(Area area, List<Stair> stairs);
        public List<ExitCapacityStruct> SumExitCapacityStructsById(List<ExitCapacityStruct> exitCapacityStructs);
    }

    public class ExitCapacityStructsService : IExitCapacityStructsService
    {
        private readonly IExitCapacityCalcService _exitCapacityCalcService;
        private readonly IStairCapacityCalcService _stairCapacityCalcService;
        private readonly IStairExitCalcService _stairExitCalcService;


        public ExitCapacityStructsService(IExitCapacityCalcService exitCapacityCalcService, IStairCapacityCalcService stairCapacityCalcService, IStairExitCalcService stairExitCalcService)
        {
            _exitCapacityCalcService = exitCapacityCalcService;
            _stairCapacityCalcService = stairCapacityCalcService;
            _stairExitCalcService = stairExitCalcService;
        }

        public List<ExitCapacityStruct> GetExitCapacityStructsForNonStairExits(List<Exit> storeyExits, List<Exit> finalExits)
        {
            var exitCapacityStructsForNonStairExits = new List<ExitCapacityStruct>();
            foreach (Exit anExit in storeyExits)
            {
                exitCapacityStructsForNonStairExits.Add(_exitCapacityCalcService.CalcExitCapacity(anExit));
            }

            foreach (Exit anExit in finalExits)
            {
                exitCapacityStructsForNonStairExits.Add(_exitCapacityCalcService.CalcExitCapacity(anExit));
            }

            return exitCapacityStructsForNonStairExits;
        }

        public Dictionary<Stair, List<ExitCapacityStruct>> GetExitCapacityStructsForStairExits(Area area, List<Stair> stairs)
        {

            var mergingflowCapacities = _stairExitCalcService.CalcMergingFlowCapacities(stairs);
            var stairExitCapacityStructs = new Dictionary<Stair, List<ExitCapacityStruct>>();

            foreach (Stair aStair in stairs)
            {

                if (area.FloorLevel > aStair.FinalExitLevel)
                {
                    //No interaction with mergeing flow, so just calculate storey exit capacities
                    var stairStoreyExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.storeyExit);

                    var storeyExitCapacity = stairStoreyExitCapacityStructs.Sum(s => s.Capacity);

                    stairExitCapacityStructs.Add(aStair, stairStoreyExitCapacityStructs);
                }

                else if (area.FloorLevel <= aStair.FinalExitLevel)
                {
                    //DECOUPLE SPLITTING OF EXIT CAPACITY ASSUMPTION WITH STRATEGY PATTERN
                    List<ExitCapacityStruct> stairFinalExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.finalExit);
                    var totalFinalExitCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.Capacity / GetNumberOfStairsServedByExit(stairs, fe));

                    List<ExitCapacityStruct> stairStoreyExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.storeyExit);
                    var totalStoreyExitCapacity = stairStoreyExitCapacityStructs.Sum(se => se.Capacity / GetNumberOfStairsServedByExit(stairs, se));

                    var mergingflowCapacity = mergingflowCapacities.Single(m => m.Key == aStair).Value;

                    if (mergingflowCapacity < totalFinalExitCapacity && mergingflowCapacity < totalStoreyExitCapacity)
                    {
                        //Exit capacity is capped by merging flow capacity...
                        var undistributedCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.Capacity);

                        //Distributes weighted merging flow capacity between the exits serving the stair.
                        stairFinalExitCapacityStructs = stairFinalExitCapacityStructs.Select(e => new ExitCapacityStruct
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Capacity = e.Capacity = mergingflowCapacity * (e.Capacity / undistributedCapacity),
                            CapacityNote = "The capacity of the exit is limited by merging flow capacity of the stair"
                        })
                        .ToList();

                        stairExitCapacityStructs.Add(aStair, stairFinalExitCapacityStructs);
                    }

                    else if (totalStoreyExitCapacity < mergingflowCapacity && totalStoreyExitCapacity < totalFinalExitCapacity)
                    {
                        stairExitCapacityStructs.Add(aStair, stairStoreyExitCapacityStructs);
                    }

                    else if (totalFinalExitCapacity < mergingflowCapacity && totalFinalExitCapacity < totalStoreyExitCapacity)
                    {
                        stairExitCapacityStructs.Add(aStair, stairFinalExitCapacityStructs);
                    }
                }
            }

            return stairExitCapacityStructs;
        }

        public List<ExitCapacityStruct> SumExitCapacityStructsById(List<ExitCapacityStruct> exitCapacityStructs)
        {
            return exitCapacityStructs.GroupBy(e => e.Id).Select(g => new ExitCapacityStruct
            {
                Id = g.Key,
                Capacity = g.Sum(e => e.Capacity),
                CapacityNote = g.First().CapacityNote, //All notes should be the same
            }).ToList();
        }

        private List<ExitCapacityStruct> GetStairExitCapacityStructs(Stair aStair, ExitType exitType)
        {
            var stairStoreyExits = aStair.Relationships.GetExits().Where(exit => exit.ExitType == exitType).ToList();

            var stairStoreyExitCapacityStructs = new List<ExitCapacityStruct>();

            stairStoreyExits.ForEach(e => stairStoreyExitCapacityStructs.Add(_exitCapacityCalcService.CalcExitCapacity(e)));

            return stairStoreyExitCapacityStructs;
        }

        private static int GetNumberOfStairsServedByExit(List<Stair> stairs, ExitCapacityStruct exit)
        {
            return stairs.Select(s => s.Relationships.GetExits()
                            .SingleOrDefault(e => e.Id == exit.Id))
                            .Where(e => e != null)
                            .ToList()
                            .Count();
        }

        private double GetTotalExitCapacity(List<Stair> stairs, Stair aStair, ExitType exitType)
        {
            List<ExitCapacityStruct> stairExitCapacityStructs = GetStairExitCapacityStructs(aStair, exitType);
            return stairExitCapacityStructs.Sum(fe => fe.Capacity / GetNumberOfStairsServedByExit(stairs, fe));
        }

    }

}
