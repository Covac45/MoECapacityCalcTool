using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Services
{
    public class ExitCapacityCalcService
    {

        private double ExitWidth;
        private DoorSwing DoorSwing;

        public ExitCapacityCalcService(Exit exit)
        {
            ExitWidth = exit.ExitWidth;
            DoorSwing = exit.DoorSwing;
        }

        public double CalcExitCapacity()
        {
            double exitCapacity = 0;

            if (ExitWidth < 750)
            {
                exitCapacity = 0;
            }
            else if (ExitWidth >= 750 && ExitWidth < 850)
            {
                exitCapacity = 60;
            }
            else if (ExitWidth >= 850 && ExitWidth < 1050)
            {
                exitCapacity = 110;
            }
            else if (ExitWidth >= 1050)
            {
                exitCapacity = 220 + (ExitWidth - 1050) / 5;
            }

            if (ExitWidth >= 750 && DoorSwing == DoorSwing.against)
            {
                exitCapacity = 60;
            }

            return exitCapacity;
        }

    }
}
