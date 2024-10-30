using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices
{
    public interface IHorizontalEscapeCapacityCalcService
    {
        List<ExitCapacityStruct> CalcExitCapacities(Area area);
        public HmoeCapacityStruct CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area);
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
        public HmoeCapacityStruct CalcTotalHMoECapacity(List<ExitCapacityStruct> exitCapacityStructs, Area area)
        {
            int numExitsFromArea = GetNumberOfEscapeRoutesFromArea(area);

            double sum = exitCapacityStructs.Select(e => e.exitCapacity).Sum();
            double max = exitCapacityStructs.Select(e => e.exitCapacity).DefaultIfEmpty(0).Max();

            return GetCappedHmoeCapacityStruct(area, numExitsFromArea, sum, max);

        }

        private HmoeCapacityStruct GetCappedHmoeCapacityStruct(Area area, int numExitsFromArea, double sum, double max)
        {
            var cap = 0;
            var hmoeCapacityNote = "";
            switch (numExitsFromArea)
            {
                case 1:
                    cap = 60;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The total horizontal means of escape capacity is limited to 60 as only a single escape route is provided to this area."; };

                    return new HmoeCapacityStruct()
                    {
                        AreaId = area.Id,
                        HmoeCapacity = CapExitCapacity(sum, cap),
                        HmoeCapacityNote = hmoeCapacityNote
                    };
                case 2:
                    cap = 600;

                    if (sum <= cap)
                    { hmoeCapacityNote = "The total horizontal means of escape capacity is limited to 600 as only two escape routes are provided to this area."; };

                    return new HmoeCapacityStruct()
                    {
                        AreaId = area.Id,
                        HmoeCapacity = CapExitCapacity(sum, cap),
                        HmoeCapacityNote = hmoeCapacityNote
                    };
                case > 2:

                    hmoeCapacityNote = "The total horizontal means of escape capacity is limited by the the capacity of escape routes.";

                    return new HmoeCapacityStruct()
                    {
                        AreaId = area.Id,
                        HmoeCapacity = sum - max,
                        HmoeCapacityNote = hmoeCapacityNote
                    };
                default:
                    hmoeCapacityNote = "No escape routes have been provided for the area.";

                    return new HmoeCapacityStruct()
                    {
                        AreaId = area.Id,
                        HmoeCapacity = 0,
                        HmoeCapacityNote = hmoeCapacityNote
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
