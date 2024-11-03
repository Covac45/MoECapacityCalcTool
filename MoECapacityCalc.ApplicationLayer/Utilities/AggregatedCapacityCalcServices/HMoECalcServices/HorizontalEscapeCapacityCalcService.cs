using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{
    public interface IHorizontalEscapeCapacityCalcService
    {
        List<ExitCapacityStruct> CalcExitCapacities(Area area);
        public CapacityStruct CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area);
    }

    public class HorizontalEscapeCapacityCalcService : IHorizontalEscapeCapacityCalcService
    {
        private readonly IExitCapacityCalcService _exitCapacityCalcService;
        private readonly IExitCapacityStructCapService _exitCapacityStructCapService;
        private readonly IStairExitCalcService _stairExitCalcService;

        public HorizontalEscapeCapacityCalcService(IExitCapacityCalcService exitCapacityCalcService,
            IExitCapacityStructCapService exitCapacityStructCapService, IStairExitCalcService stairExitCalcService)
        {
            _exitCapacityCalcService = exitCapacityCalcService;
            _exitCapacityStructCapService = exitCapacityStructCapService;
            _stairExitCalcService = stairExitCalcService;
        }

        public List<ExitCapacityStruct> CalcExitCapacities(Area area)
        {
            var stairs = area.Relationships.GetStairs();

            var exits = area.Relationships.GetExits();

            var storeyExits = exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
            var finalExits = exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
            var altExits = exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

            //For HMoE not associated with stairs
            var nonStairExitCapacityStructs = GetExitCapacityStructsForNonStairExits(storeyExits, finalExits);

            //ForHMoEAssociated with Stairs
            var allExitCapacityStructs = AddExitCapacityStructsForStairExits(area, stairs, nonStairExitCapacityStructs);

            var cappedExitCapacityStructs = _exitCapacityStructCapService.GetCappedExitCapacityStructs(allExitCapacityStructs);

            return cappedExitCapacityStructs;
        }

        //Implements discounting logic for multiple exits (i.e. remove the most capacious exit)
        //Also implements capping logic based on number of storey exits (single exit: 60 people, two exits: 600 people)
        public CapacityStruct CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            int numExitsFromArea = GetNumberOfEscapeRoutesFromArea(area);

            double sum = exitCapacityStructs.Select(e => e.Capacity).Sum();
            double max = exitCapacityStructs.Select(e => e.Capacity).DefaultIfEmpty().Max();

            return GetCappedHmoeCapacityStruct(area, numExitsFromArea, sum, max);

        }

        private CapacityStruct GetCappedHmoeCapacityStruct(Area area, int numExitsFromArea, double sum, double max)
        {
            var cap = 0;
            var hmoeCapacityNote = "";
            switch (numExitsFromArea)
            {
                case 1:
                    cap = 60;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The means of escape capacity of this area is limited to 60 as only a single escape route is provided to this area."; };

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = CapExitCapacity(sum, cap),
                        CapacityNote = hmoeCapacityNote
                    };
                case 2:
                    cap = 600;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The means of escape capacity of this area is limited to 600 as only two escape routes are provided to this area."; };

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = CapExitCapacity(sum, cap),
                        CapacityNote = hmoeCapacityNote
                    };
                case > 2:

                    hmoeCapacityNote = "The means of escape capacity of this area is limited by the the capacity of escape routes. See escape route capacity assessment for further information.";

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = sum - max,
                        CapacityNote = hmoeCapacityNote
                    };
                default:
                    hmoeCapacityNote = "No escape routes have been provided for the area.";

                    return new CapacityStruct()
                    {
                        Id = area.Id,
                        Capacity = 0,
                        CapacityNote = hmoeCapacityNote
                    };
            }
        }

        private static int GetNumberOfEscapeRoutesFromArea(Area area)
        {
            var numStairsServingArea = area.Relationships.GetStairs()
                                                        .Select(s => s)
                                                        .Where(s => s.Relationships.GetExits().Where(e => e.ExitType == ExitType.storeyExit) != null)
                                                        .Count();

            var numNonStairExitsServingArea = area.Relationships.GetExits().Count();

            int numEscapeRoutesFromArea = numNonStairExitsServingArea + numStairsServingArea;
            return numEscapeRoutesFromArea;
        }

        private List<ExitCapacityStruct> GetExitCapacityStructsForNonStairExits(List<Exit> storeyExits, List<Exit> finalExits)
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

        private List<ExitCapacityStruct> AddExitCapacityStructsForStairExits(Area area, List<Stair> stairs, List<ExitCapacityStruct> exitCapacityStructs)
        {
            var mergingflowCapacities = _stairExitCalcService.CalcMergingFlowCapacities(stairs);
            var mergingFlowCapacityStructs = new List<ExitCapacityStruct>();

            foreach (Stair aStair in stairs)
            {

                if (area.FloorLevel != aStair.FinalExitLevel)
                {
                    //No interaction with mergeing flow, so just calculate storey exit capacities
                    var stairStoreyExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.storeyExit);

                    exitCapacityStructs.AddRange(stairStoreyExitCapacityStructs);
                }

                else if (area.FloorLevel == aStair.FinalExitLevel)
                {
                    //DECOUPLE SPLITTING OF EXIT CAPACITY ASSUMPTION WITH STRATEGY PATTERN
                    List<ExitCapacityStruct> stairFinalExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.finalExit);
                    var totalFinalExitCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.Capacity / GetNumberOfStairServedByExit(stairs, fe));

                    List<ExitCapacityStruct> stairStoreyExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.storeyExit);
                    var totalStoreyExitCapacity = stairStoreyExitCapacityStructs.Sum(se => se.Capacity / GetNumberOfStairServedByExit(stairs, se));

                    var mergingflowCapacity = mergingflowCapacities.Single(m => m.Key == aStair).Value;

                    if (mergingflowCapacity < totalFinalExitCapacity && mergingflowCapacity < totalStoreyExitCapacity)
                    {
                        //Exit capacity is capped by merging flow capacity...
                        var undistributedCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.Capacity);

                        //Distributes weighted merging flow capacity between the exits serving the stair.
                        stairFinalExitCapacityStructs = stairFinalExitCapacityStructs.Select(e => new ExitCapacityStruct
                        {
                            Id = e.Id,
                            Capacity = e.Capacity = mergingflowCapacity * (e.Capacity / undistributedCapacity),
                            CapacityNote = "The capacity of the exit is limited by merging flow capacity of the stair"
                        })
                            .ToList();

                        mergingFlowCapacityStructs.AddRange(stairFinalExitCapacityStructs);
                    }

                    else if (totalStoreyExitCapacity < mergingflowCapacity && totalStoreyExitCapacity < totalFinalExitCapacity)
                    {
                        mergingFlowCapacityStructs.AddRange(stairStoreyExitCapacityStructs);
                    }

                    else if (totalFinalExitCapacity < mergingflowCapacity && totalFinalExitCapacity < totalStoreyExitCapacity)
                    {
                        mergingFlowCapacityStructs.AddRange(stairFinalExitCapacityStructs);
                    }
                }
            }

            var summedMergingFlowCapacityStructs = SumMergingFLowCapacityStructsById(mergingFlowCapacityStructs);

            exitCapacityStructs.AddRange(summedMergingFlowCapacityStructs);

            return exitCapacityStructs;
        }

        //DECOUPLE EXIT SPLITTING ASSUMPTION USING STRATEGY PATTERN
        private static int GetNumberOfStairServedByExit(List<Stair> stairs, ExitCapacityStruct fe)
        {
            return stairs.Select(s => s.Relationships.GetExits()
                            .SingleOrDefault(e => e.Id == fe.Id))
                            .Where(e => e != null)
                            .ToList()
                            .Count();
        }

        private List<ExitCapacityStruct> GetStairExitCapacityStructs(Stair aStair, ExitType exitType)
        {
            var stairStoreyExits = aStair.Relationships.GetExits().Where(exit => exit.ExitType == exitType).ToList();

            var stairStoreyExitCapacityStructs = new List<ExitCapacityStruct>();

            stairStoreyExits.ForEach(e => stairStoreyExitCapacityStructs.Add(_exitCapacityCalcService.CalcExitCapacity(e)));

            return stairStoreyExitCapacityStructs;
        }

        private static List<ExitCapacityStruct> SumMergingFLowCapacityStructsById(List<ExitCapacityStruct> mergingFlowCapacityStructs)
        {
            return mergingFlowCapacityStructs.GroupBy(e => e.Id).Select(g => new ExitCapacityStruct
            {
                Id = g.Key,
                Capacity = g.Sum(e => e.Capacity),
                CapacityNote = g.First().CapacityNote, //All notes should be the same
            }).ToList();
        }


        private double CapExitCapacity(double totalExitCapacity, double cap)
        {
            return Math.Min(totalExitCapacity, cap);
        }

        private double GetTotalExitCapacity(List<Stair> stairs, Stair aStair, ExitType exitType)
        {
            List<ExitCapacityStruct> stairExitCapacityStructs = GetStairExitCapacityStructs(aStair, exitType);
            return stairExitCapacityStructs.Sum(fe => fe.Capacity / GetNumberOfStairServedByExit(stairs, fe));
        }

            

    }
}
