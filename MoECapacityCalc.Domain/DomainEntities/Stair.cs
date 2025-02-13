using MoECapacityCalc.DomainEntities.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.DomainEntities
{
    public class Stair : MeansOfEscapeEntity<Stair>
    {
        public double StairWidth { get; set; }
        public int FloorsServed { get; set; }
        public int FinalExitLevel { get; set; }
        public bool IsSmokeProtected { get; set; }

        public Stair() { }

        public Stair(string name, double width, int floorsServedPerEvacuationPhase, int finalExitLevel, bool isSmokeProtected)
        {
            Id = Guid.NewGuid();
            Name = name;
            StairWidth = width;
            FloorsServed = floorsServedPerEvacuationPhase;
            FinalExitLevel = finalExitLevel;
            IsSmokeProtected = isSmokeProtected;
            Relationships = new RelationshipSet<Stair>();
        }
    }
}
