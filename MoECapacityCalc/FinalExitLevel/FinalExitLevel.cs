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

            return (80 * (exitWidth / 1000) - 60 * (stairWidth / 1000)) * 2.5;
        }

        public void CalcFinalExitLevelCapacity()
        {            
            double mergingFlowCapacity = this.CalcMergingFlowCapacity();

            double storeyExitCapacity = this.storeyExit.CalcExitCapacity();

            double finalExitCapacity = this.finalExit.CalcExitCapacity();

            var capacities = new List<double> {mergingFlowCapacity, storeyExitCapacity, finalExitCapacity};

            double FinalExitLevelCapacity = capacities.Min();

        }
    }
}
