using MoECapacityCalc.Database.Interfaces;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.Exits
{
    public class Exit : Entity
    {
        public Guid Id;
        public string ExitName { get; set; }
        public ExitType ExitType { get; set; }
        public double ExitWidth { get; set; }
        public DoorSwing DoorSwing { get; set; }
        public RelationshipSet<Exit> Relationships { get; set; }

        public Exit() { }

        public Exit(string exitName, ExitType exitType, DoorSwing doorSwing, double exitWidth)
        {
            Id = Guid.NewGuid();
            ExitName = exitName;
            ExitType = exitType;
            ExitWidth = exitWidth;
            DoorSwing = doorSwing;

            Relationships = new RelationshipSet<Exit>();
        }

    }
}
