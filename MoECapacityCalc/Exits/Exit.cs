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
        public ExitType Type;
        public double Width;
        public DoorSwing Swing;

        public Exit(string name, ExitType exitType, DoorSwing doorSwing, double exitWidth)
        {
            ExitName = name;
            Type = exitType;
            Width = exitWidth;
            Swing = doorSwing;
        }

        public double CalcExitCapacity()
        {
            double exitCapacity = 0;

            if (Width < 750)
            {
                exitCapacity = 0;
            }
            else if (Width >= 750 && Width < 850)
            {
                exitCapacity = 60;
            }
            else if (Width >= 850 && Width < 1050)
            {
                exitCapacity = 110;
            }
            else if (Width >= 1050)
            {
                exitCapacity = 220 + (Width - 1050) / 5;
            }

            if (Width >= 750 && Swing == DoorSwing.against)
            {
                exitCapacity = 60;
            }

            return exitCapacity;
        }

    }
}
