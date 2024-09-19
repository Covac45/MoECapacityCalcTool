using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Stairs.StairFinalExits
{
    public class StairFinalExit
    {
        public Stair Stair;
        public List<Exit> StoreyExits;

        public StairFinalExit(Stair stair, List<Exit> storeyExits)
        {
            Stair = stair;
            StoreyExits = storeyExits;
        }

        public StairFinalExit(Stair stair)
        {
            Stair = stair;
        }

        public double CalcMergingFlowCapacity()
        {
            List<double> finalExitWidths = new List<double>();
            
            foreach (Exit anExit in Stair.FinalExits)
            {
                //must add logic to split clear width of shared final exits amongst stairs that share them!
                finalExitWidths.Add(anExit.Width);
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

        /*public double CalcFinalExitLevelCapacity()
        {
            double mergingFlowCapacity = CalcMergingFlowCapacity();

            double storeyExitCapacity = storeyExit.CalcExitCapacity();

            double finalExitCapacity = stair.FinalExits.CalcExitCapacity();

            var capacities = new List<double> { mergingFlowCapacity, storeyExitCapacity, finalExitCapacity };

            return capacities.Min();
        }*/

    }
}
