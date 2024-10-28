using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Services;

namespace MoECapacityCalc.Utilities.CalcServices
{
    public interface IHorizontalEscapeCapacityCalcService
    {
        List<ExitCapacityStruct> CalcExitCapacities(Area area);
        double CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs);
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
        public double CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs)
        {
            int numExits = exitCapacityStructs.Count();

            double sum = exitCapacityStructs.Select(e => e.exitCapacity).Sum();
            double max = exitCapacityStructs.Select(e => e.exitCapacity).Max();


            switch (numExits)
            {
                case 1:
                    return CapExitCapacity(sum, 60);
                case 2:
                    return CapExitCapacity(sum - max, 600);
                case > 2:
                    return sum - max;
                default:
                    return 0;
                    throw new Exception("The number of exits prodived to this area is less than 1. This is not supported");
            }

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
                    List<ExitCapacityStruct> stairFinalExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.finalExit);
                    var totalFinalExitCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.exitCapacity / GetNumberOfStairServedByExit(stairs, fe));

                    List<ExitCapacityStruct> stairStoreyExitCapacityStructs = GetStairExitCapacityStructs(aStair, ExitType.storeyExit);
                    var totalStoreyExitCapacity = stairStoreyExitCapacityStructs.Sum(se => se.exitCapacity / GetNumberOfStairServedByExit(stairs, se));

                    var mergingflowCapacity = mergingflowCapacities.Single(m => m.Key == aStair).Value;

                    if (mergingflowCapacity < totalFinalExitCapacity && mergingflowCapacity < totalStoreyExitCapacity)
                    {
                        //Exit capacity is capped by merging flow capacity...
                        var undistributedCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.exitCapacity);

                        //Distributes weighted merging flow capacity between the exits serving the stair.
                        stairFinalExitCapacityStructs = stairFinalExitCapacityStructs.Select(e => new ExitCapacityStruct
                            {
                                ExitId = e.ExitId,
                                exitCapacity = e.exitCapacity = mergingflowCapacity * (e.exitCapacity / undistributedCapacity),
                                capacityNote = "The capacity of the exit is limited by merging flow capacity of the stair"
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

        private static int GetNumberOfStairServedByExit(List<Stair> stairs, ExitCapacityStruct fe)
        {
            return stairs.Select(s => s.Relationships.GetExits()
                            .SingleOrDefault(e => e.Id == fe.ExitId))
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
            return mergingFlowCapacityStructs.GroupBy(e => e.ExitId).Select(g => new ExitCapacityStruct
            {
                ExitId = g.Key,
                exitCapacity = g.Sum(e => e.exitCapacity),
                capacityNote = g.First().capacityNote, //All notes should be the same
            }).ToList();
        }


        private double CapExitCapacity(double totalExitCapacity, double cap)
        {
            return Math.Min(totalExitCapacity, cap);
        }


    }
}
