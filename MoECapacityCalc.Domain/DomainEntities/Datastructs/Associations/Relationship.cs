using MoECapacityCalc.Domain.DomainEntities.Datastructs;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Relationship<T1,T2>
    {
        public T1 Object1 { get; }
        public RelativeDirection RelativeDirection { get; } 
        public T2 Object2 { get; }

        public Relationship() { }

        public Relationship(T1 object1, RelativeDirection relativeDirection, T2 object2) 
        {
            Object1 = object1;
            RelativeDirection = relativeDirection;
            Object2 = object2;
        }
    }
}
