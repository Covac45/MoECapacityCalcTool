using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.Exits
{
    public class Exit : IExit
    {
        public string ExitName;
        public ExitType ExitType;
        public double ExitWidth;
        public DoorSwing DoorSwing;

        public Exit(string exitName, ExitType exitType, DoorSwing doorSwing, double exitWidth)
        {
            ExitName = exitName;
            ExitType = exitType;
            ExitWidth = exitWidth;
            DoorSwing = doorSwing;
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
