using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.Exits
{
    public class Exit : MeansOfEscapeEntity<Exit>
    {
        public ExitType ExitType { get; set; }
        public double ExitWidth { get; set; }
        public DoorSwing DoorSwing { get; set; }

        public Exit() { }

        public Exit(string exitName, ExitType exitType, DoorSwing doorSwing, double exitWidth)
        {
            Id = Guid.NewGuid();
            Name = exitName;
            ExitType = exitType;
            ExitWidth = exitWidth;
            DoorSwing = doorSwing;
            Relationships = new RelationshipSet<Exit>();
        }

    }
}
