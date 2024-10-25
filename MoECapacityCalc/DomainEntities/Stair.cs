using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.DomainEntities
{
    public class Stair : MeansOfEscapeEntity<Stair>
    {
        public double StairWidth { get; set; }
        public int FloorsServed { get; set; }
        public int FinalExitLevel { get; set; }

        public Stair() { }

        public Stair(string name, double width, int floorsServed, int finalExitLevel)
        {
            Id = Guid.NewGuid();
            Name = name;
            StairWidth = width;
            FloorsServed = floorsServed;
            FinalExitLevel = finalExitLevel;
            Relationships = new RelationshipSet<Stair>();
        }

    }
}
