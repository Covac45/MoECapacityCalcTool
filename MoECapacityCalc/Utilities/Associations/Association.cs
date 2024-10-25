using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Association : Entity
    {
        public Guid AssociationId { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectType { get; set; }

        public Association() { }

        public Association(Guid objectId, string objectType, Guid subjectId, string subjectType)
        {
            AssociationId = Guid.NewGuid();
            ObjectId = objectId;
            ObjectType = objectType;
            SubjectId = subjectId;
            SubjectType = subjectType;
        }

        public Association(Stair stair, Exit exit)
        {
            AssociationId = Guid.NewGuid();
            ObjectId = stair.Id;
            ObjectType = stair.GetType().Name;
            SubjectId = exit.Id;
            SubjectType = exit.GetType().Name;
        }

        public Association(Exit exit1, Exit exit2)
        {
            AssociationId = Guid.NewGuid();
            ObjectId = exit1.Id;
            ObjectType = exit2.GetType().Name;
            SubjectId = exit2.Id;
            SubjectType = exit2.GetType().Name;
        }

        public Association(Area area, Exit exit)
        {
            AssociationId = Guid.NewGuid();
            ObjectId = area.Id;
            ObjectType = exit.GetType().Name;
            SubjectId = exit.Id;
            SubjectType = exit.GetType().Name;
        }

        public Association(Area area, Stair stair)
        {
            AssociationId = Guid.NewGuid();
            ObjectId = area.Id;
            ObjectType = stair.GetType().Name;
            SubjectId = stair.Id;
            SubjectType = stair.GetType().Name;
        }
    }
}
