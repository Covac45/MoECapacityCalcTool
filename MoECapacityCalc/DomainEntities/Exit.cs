using MoECapacityCalc.DomainEntities.Abstractions;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.DomainEntities
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
