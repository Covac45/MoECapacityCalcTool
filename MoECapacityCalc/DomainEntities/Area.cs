using MoECapacityCalc.DomainEntities.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.DomainEntities
{
    public class Area : MeansOfEscapeEntity<Area>
    {
        public int FloorLevel { get; set; }
        public bool IsSprinklered { get; set; }

        public Area() { }

        public Area(int floorLevel, string areaName, bool isSprinklered)
        {
            Id = Guid.NewGuid();
            Name = areaName;
            FloorLevel = floorLevel;
            Relationships = new RelationshipSet<Area>();
        }

    }
}
