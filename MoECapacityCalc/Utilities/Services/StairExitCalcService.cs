using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Services
{
    public class StairExitCalcService
    {
        private Stair Stair;
        private List<Exit> StoreyExits { get; set; }
        private List<Exit> FinalExits { get; set; }

        MoEContext context = new();

        public StairExitCalcService(Stair stair)
        {
            Stair = stair;

            StoreyExits = Stair.Relationships.GetExits().Where(e => e.ExitType == ExitType.storeyExit).ToList();
            FinalExits = Stair.Relationships.GetExits().Where(e => e.ExitType == ExitType.finalExit).ToList();
        }

        public double TotalStoreyExitCapacity()
        {
            List<double> storeyExitCapacities = new List<double>();
            foreach (Exit anExit in StoreyExits)
            {
                storeyExitCapacities.Add(
                    new ExitCapacityCalcService(anExit).CalcExitCapacity());
            }

            double storeyExitCapacity = storeyExitCapacities.Sum();
            return storeyExitCapacity;
        }

        public double TotalFinalExitCapacity()
        {
            List<double> finalExitCapacities = new List<double>();
            foreach (Exit anExit in FinalExits)
            {
                finalExitCapacities.Add(
                    new ExitCapacityCalcService(anExit).CalcExitCapacity());
            }

            double finalExitCapacity = finalExitCapacities.Sum();
            return finalExitCapacity;
        }

        public double CalcMergingFlowCapacity()
        {
            List<double> finalExitWidths = new List<double>();

            foreach (Exit anExit in FinalExits)
            {
                //must add logic to split clear width of shared final exits amongst stairs that share them!
                finalExitWidths.Add(anExit.ExitWidth);
            }

            double totalFinalExitWidth = finalExitWidths.Sum();

            double mergingFlowCapacity = (80 * (totalFinalExitWidth / 1000) - 60 * (Stair.StairWidth / 1000)) * 2.5;

            switch (mergingFlowCapacity)
            {
                case <= 0:
                    return mergingFlowCapacity = 0;
                case > 0:
                    return mergingFlowCapacity;
                default:
                    throw new NotSupportedException("The mering flow capacity has been calculated as NaN");
            }
        }

            public double CalcFinalExitLevelCapacity()
        {
            //Calculate total storey exit and final exit capacity
            double storeyExitCapacity = this.TotalStoreyExitCapacity();
            double finalExitCapacity = this.TotalFinalExitCapacity();

            //Calculate merging flow capacity
            double mergingFlowCapacity = this.CalcMergingFlowCapacity();

            //calculate limiting factor
            var capacities = new List<double> { mergingFlowCapacity, storeyExitCapacity, finalExitCapacity };

            return capacities.Min();
        }

        public double CalcStoreyExitLevelCapacity()
        {
            double stairCapacityPerFloor = new StairCapacityCalcService(Stair).CalcStairCapacityPerFloor();

            //Calculate total storey exit capacity
            StairExitCalcService exitCapacityCalcs = new StairExitCalcService(Stair);
            double storeyExitCapacity = exitCapacityCalcs.TotalStoreyExitCapacity();

            var capacities = new List<double> { stairCapacityPerFloor, storeyExitCapacity };

            return capacities.Min();
        }

    }
}
