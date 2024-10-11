using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.Exits
{
    public class Exit
    {
        public string ExitName;
        public ExitType ExitType;
        public double ExitWidth;
        public DoorSwing DoorSwing;
        public Associations? Associations;

        public Exit(string exitName, ExitType exitType, DoorSwing doorSwing, double exitWidth, Associations? associations = null)
        {
            ExitName = exitName;
            ExitType = exitType;
            ExitWidth = exitWidth;
            DoorSwing = doorSwing;
            Associations = associations;
        }

    }
}
