using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.DomainEntities.Abstractions
{
    public abstract class MeansOfEscapeEntity<T> : Entity
    {
        public string Name { get; set; }
        public RelationshipSet<T> Relationships { get; set; }
    }
}