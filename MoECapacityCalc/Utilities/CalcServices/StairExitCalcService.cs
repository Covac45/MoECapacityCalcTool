using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Services
{
    public class StairExitCalcService
    {

        public StairExitCalcService() { }

        public double TotalStoreyExitCapacity(Stair stair)
        {
            var storeyExits = stair.Relationships.GetExits().Where(e => e.ExitType == ExitType.storeyExit).ToList();

            List<double> storeyExitCapacities = new List<double>();
            foreach (Exit anExit in storeyExits)
            {
                storeyExitCapacities.Add(
                    new ExitCapacityCalcService().CalcExitCapacity(anExit).exitCapacity);
            }

            double storeyExitCapacity = storeyExitCapacities.Sum();
            return storeyExitCapacity;
        }

        public double TotalFinalExitCapacity(Stair stair)
        {
            var finalExits = stair.Relationships.GetExits().Where(e => e.ExitType == ExitType.finalExit).ToList();

            List<double> finalExitCapacities = new List<double>();
            foreach (Exit anExit in finalExits)
            {
                finalExitCapacities.Add(new ExitCapacityCalcService().CalcExitCapacity(anExit).exitCapacity);
            }

            double finalExitCapacity = finalExitCapacities.Sum();
            return finalExitCapacity;
        }


        public Dictionary<Stair, double> CalcMergingFlowCapacities(List<Stair> stairs)
        {

            var allStairRelationships = new List<Relationship<Stair, Exit>>();

            stairs.ForEach(s => allStairRelationships.AddRange(s.Relationships.ExitRelationships));

            var allStairFinalExits = allStairRelationships.Select(r => r.Object2).Where(e => e.ExitType == ExitType.finalExit).ToList();




            double totalFinalExitWidth = 0;
            double mergingFlowCapacity = 0;
            Dictionary<Stair, double> mergingFlowCapacities = new();

            foreach (var stair in stairs)
            {
                totalFinalExitWidth = 0;

                foreach (var finalExit in stair.Relationships.ExitRelationships.Select(r => r.Object2).Where(e => e.ExitType == ExitType.finalExit))
                {

                    int stairSharingCount = stairs.Count(s => s.Relationships.ExitRelationships
                                                    .Select(r => r.Object2)
                                                    .Where(e => e.ExitType == ExitType.finalExit)
                                                    .Contains(finalExit));
                    if (stairSharingCount > 1)
                    {
                        totalFinalExitWidth += finalExit.ExitWidth / stairSharingCount;
                    }
                    else
                    {
                        totalFinalExitWidth += finalExit.ExitWidth / stairSharingCount;
                    }


                }

                mergingFlowCapacity = (80 * (totalFinalExitWidth / 1000) - 60 * (stair.StairWidth / 1000)) * 2.5;

                if (mergingFlowCapacity <= 0)
                    mergingFlowCapacity = 0;

                mergingFlowCapacities.Add(stair, mergingFlowCapacity);
            }

            return mergingFlowCapacities;
        }

        public double CalcFinalExitLevelCapacity(Stair stair)
        {
            //Calculate total storey exit and final exit capacity
            double storeyExitCapacity = TotalStoreyExitCapacity(stair);
            double finalExitCapacity = TotalFinalExitCapacity(stair);

            //Calculate merging flow capacity
            double mergingFlowCapacity = this.CalcMergingFlowCapacities(new List<Stair>() { stair }).Single(s => s.Key == stair).Value;

            //calculate limiting factor
            var capacities = new List<double> { mergingFlowCapacity, storeyExitCapacity, finalExitCapacity };

            return capacities.Min();
        }

        public double CalcStoreyExitLevelCapacity(Stair stair)
        {
            double stairCapacityPerFloor = new StairCapacityCalcService().CalcStairCapacityPerFloor(stair);

            //Calculate total storey exit capacity
            StairExitCalcService exitCapacityCalcs = new StairExitCalcService();
            double storeyExitCapacity = exitCapacityCalcs.TotalStoreyExitCapacity(stair);

            var capacities = new List<double> { stairCapacityPerFloor, storeyExitCapacity };

            return capacities.Min();
        }

    }
}
