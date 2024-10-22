using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.Exits
{
    public class Exit
    {
        public Guid ExitId;
        public string ExitName { get; set; }
        public ExitType ExitType { get; set; }
        public double ExitWidth { get; set; }
        public DoorSwing DoorSwing { get; set; }
        public RelationshipSet<Exit> Relationships { get; set; }

        public Exit() { }

        public Exit(string exitName, ExitType exitType, DoorSwing doorSwing, double exitWidth)
        {
            ExitId = Guid.NewGuid();
            ExitName = exitName;
            ExitType = exitType;
            ExitWidth = exitWidth;
            DoorSwing = doorSwing;

            Relationships = new RelationshipSet<Exit>();
        }

    }
}
