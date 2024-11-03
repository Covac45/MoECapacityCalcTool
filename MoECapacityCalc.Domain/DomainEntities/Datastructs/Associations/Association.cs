using MoECapacityCalc.Domain.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Association
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public RelativeDirection RelativeDirection { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectType { get; set; }

        public Association() { }

        public Association(Guid objectId, string objectType, RelativeDirection relativeDirection, Guid subjectId, string subjectType)
        {
            Id = Guid.NewGuid();
            ObjectId = objectId;
            ObjectType = objectType;
            RelativeDirection = relativeDirection;
            SubjectId = subjectId;
            SubjectType = subjectType;
        }

        public Association(Stair stair, RelativeDirection relativeDirection, Exit exit)
        {
            Id = Guid.NewGuid();
            ObjectId = stair.Id;
            ObjectType = stair.GetType().Name;
            RelativeDirection = relativeDirection;
            SubjectId = exit.Id;
            SubjectType = exit.GetType().Name;
        }

        public Association(Exit exit1, RelativeDirection relativeDirection, Exit exit2)
        {
            Id = Guid.NewGuid();
            ObjectId = exit1.Id;
            ObjectType = exit1.GetType().Name;
            RelativeDirection = relativeDirection;
            SubjectId = exit2.Id;
            SubjectType = exit2.GetType().Name;
        }

        public Association(Area area, RelativeDirection relativeDirection, Exit exit)
        {
            Id = Guid.NewGuid();
            ObjectId = area.Id;
            ObjectType = area.GetType().Name;
            RelativeDirection = relativeDirection;
            SubjectId = exit.Id;
            SubjectType = exit.GetType().Name;
        }

        public Association(Area area, RelativeDirection relativeDirection, Stair stair)
        {
            Id = Guid.NewGuid();
            ObjectId = area.Id;
            ObjectType = area.GetType().Name;
            RelativeDirection = relativeDirection;
            SubjectId = stair.Id;
            SubjectType = stair.GetType().Name;
        }
    }
}
