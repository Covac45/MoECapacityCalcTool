using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.FinalExitLevel
{
    public class FinalExitLevel
    {
        public Stair stair;
        public Exit finalExit;
        public Exit storeyExit;

        public FinalExitLevel(Stair stairObj, Exit finalExitObj, Exit storeyExitObj)
        {
            stair = stairObj;
            finalExit = finalExitObj;
            storeyExit = storeyExitObj;
        }

        public FinalExitLevel(Stair stairObj, Exit finalExitObj)
        {
            stair = stairObj;
            finalExit = finalExitObj;
        }

        public double CalcMergingFlowCapacity()
        {
            double exitWidth = finalExit.Width;
            double stairWidth = stair.StairWidth;

            double mergingFlowCapacity = (80 * (exitWidth / 1000) - 60 * (stairWidth / 1000)) * 2.5;

            switch(mergingFlowCapacity)
            {
                case <= 0:
                    return mergingFlowCapacity = 0;
                    break;
                case > 0:
                    return mergingFlowCapacity;
                    break;
                default:
                    throw new NotSupportedException("The mering flow capacity has been calculated as NaN");
            }
            
        }

        public double CalcFinalExitLevelCapacity()
        {            
            double mergingFlowCapacity = this.CalcMergingFlowCapacity();

            double storeyExitCapacity = this.storeyExit.CalcExitCapacity();

            double finalExitCapacity = this.finalExit.CalcExitCapacity();

            var capacities = new List<double> {mergingFlowCapacity, storeyExitCapacity, finalExitCapacity};

            return capacities.Min();
            
        }
    }
}
