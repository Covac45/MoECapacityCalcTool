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

        public void CalcFinalExitLevelCapacity()
        {

        }
    }
}
