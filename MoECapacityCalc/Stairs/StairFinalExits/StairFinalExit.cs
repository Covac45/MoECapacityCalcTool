using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;
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
        public Stair stair;
        public Exit storeyExit;

        public StairFinalExit(Stair stairObj, Exit storeyExitObj)
        {
            stair = stairObj;
            storeyExit = storeyExitObj;
        }

        public StairFinalExit(Stair stairObj)
        {
            stair = stairObj;
        }

        public double CalcMergingFlowCapacity()
        {
            double exitWidth = stair.FinalExit.Width;
            double stairWidth = stair.StairWidth;

            double mergingFlowCapacity = (80 * (exitWidth / 1000) - 60 * (stairWidth / 1000)) * 2.5;

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
            double mergingFlowCapacity = CalcMergingFlowCapacity();

            double storeyExitCapacity = storeyExit.CalcExitCapacity();

            double finalExitCapacity = stair.FinalExit.CalcExitCapacity();

            var capacities = new List<double> { mergingFlowCapacity, storeyExitCapacity, finalExitCapacity };

            return capacities.Min();
        }

    }
}
