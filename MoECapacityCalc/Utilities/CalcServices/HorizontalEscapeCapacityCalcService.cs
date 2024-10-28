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
    public class HorizontalEscapeCapacityCalcService
    {


        public HorizontalEscapeCapacityCalcService()
        {

        }

        public List<ExitCapacityStruct> CalcExitCapacities(Area area)
        {
            var stairs = area.Relationships.GetStairs();

            var exits = area.Relationships.GetExits();

            var storeyExits = exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
            var finalExits = exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
            var altExits = exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

            var exitCapacityStructs = new List<ExitCapacityStruct>();

            //For HMoE not associated with stairs
            InsertExitCapacityStructsForNonStairExits(storeyExits, finalExits, exitCapacityStructs);

            //ForHMoEAssociated with Stairs
            InsertExitCapacityStructsForStairExits(area, stairs, exitCapacityStructs);

            exitCapacityStructs = new ExitCapacityStructCapService().GetCappedExitCapacityStructs(exitCapacityStructs);

            return exitCapacityStructs;
        }

        private static void InsertExitCapacityStructsForNonStairExits(List<Exit> storeyExits, List<Exit> finalExits, List<ExitCapacityStruct> exitCapacityStructs)
        {
            foreach (Exit anExit in storeyExits)
            {
                exitCapacityStructs.Add(new ExitCapacityCalcService().CalcExitCapacity(anExit));
            }

            foreach (Exit anExit in finalExits)
            {
                exitCapacityStructs.Add(new ExitCapacityCalcService().CalcExitCapacity(anExit));
            }
        }

        private static List<ExitCapacityStruct> InsertExitCapacityStructsForStairExits(Area area, List<Stair> stairs, List<ExitCapacityStruct> exitCapacityStructs)
        {
            var mergingflowCapacities = new StairExitCalcService().CalcMergingFlowCapacities(stairs);
            var mergingFlowCapacityStructs = new List<ExitCapacityStruct>();

            foreach (Stair aStair in stairs)
            {

                if (area.FloorLevel != aStair.FinalExitLevel)
                {
                    InsertExitCapacityStructsForStairStoreyExits(exitCapacityStructs, aStair);
                }

                else if (area.FloorLevel == aStair.FinalExitLevel)
                {
                    List<ExitCapacityStruct> stairFinalExitCapacityStructs = GetStairFinalExitCapacityStructs(aStair);

                    var totalFinalExitCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.exitCapacity /
                                                                                stairs.Select(s => s.Relationships.GetExits()
                                                                                        .SingleOrDefault(e => e.Id == fe.ExitId))
                                                                                        .Where(e => e != null).ToList().Count());

                    var mergingflowCapacity = mergingflowCapacities.Single(m => m.Key == aStair).Value;

                    if (totalFinalExitCapacity > mergingflowCapacity)
                    {
                        var undistributedCapacity = stairFinalExitCapacityStructs.Sum(fe => fe.exitCapacity);

                        //Distributes a weighted the merging flow capacity between the exits serving the stair.
                        stairFinalExitCapacityStructs.ForEach(e => e.exitCapacity = mergingflowCapacity *
                                                                    (e.exitCapacity / undistributedCapacity));

                        stairFinalExitCapacityStructs.ForEach(e => e.capacityNote = "The capacity of the exit is limited by merging flow capacity of the stair");
                    }
                    
                    mergingFlowCapacityStructs.AddRange(stairFinalExitCapacityStructs);
                }
            }

            var summedMergingFlowCapacityStructs = SumMergingFLowCapacityStructsById(mergingFlowCapacityStructs);

            exitCapacityStructs.AddRange(summedMergingFlowCapacityStructs);

            return exitCapacityStructs;
        }


        private static void InsertExitCapacityStructsForStairStoreyExits(List<ExitCapacityStruct> exitCapacityStructs, Stair aStair)
        {
            var stairStoreyExits = aStair.Relationships.GetExits().Where(exit => exit.ExitType == ExitType.storeyExit).ToList();

            stairStoreyExits.ForEach(e => exitCapacityStructs.Add(new ExitCapacityCalcService().CalcExitCapacity(e)));
        }



        private static List<ExitCapacityStruct> GetStairFinalExitCapacityStructs(Stair aStair)
        {
            var stairFinalExits = aStair.Relationships.GetExits().Where(exit => exit.ExitType == ExitType.finalExit).ToList();

            List<ExitCapacityStruct> stairFinalExitCapacityStructs = new();

            stairFinalExits.ForEach(exit => stairFinalExitCapacityStructs.Add(new ExitCapacityCalcService().CalcExitCapacity(exit)));
            return stairFinalExitCapacityStructs;
        }



        private static List<ExitCapacityStruct> SumMergingFLowCapacityStructsById(List<ExitCapacityStruct> mergingFlowCapacityStructs)
        {
            return mergingFlowCapacityStructs.GroupBy(e => e.ExitId).Select(g => new ExitCapacityStruct
            {
                ExitId = g.Key,
                exitCapacity = g.Sum(e => e.exitCapacity),
                capacityNote = "The capacity of the exit is limited by merging flow capacity of the stair"
            }).ToList();
        }



        



        


        


        //implements discounting logic for multiple exits (i.e. remove the most capacious exit)
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


        private double CapExitCapacity(double totalExitCapacity, double cap)
        {
            return Math.Min(totalExitCapacity, cap);
        }


    }
}
