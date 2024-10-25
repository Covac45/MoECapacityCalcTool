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
    public class Association
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectType { get; set; }

        public Association() { }

        public Association(Guid objectId, string objectType, Guid subjectId, string subjectType)
        {
            Id = Guid.NewGuid();
            ObjectId = objectId;
            ObjectType = objectType;
            SubjectId = subjectId;
            SubjectType = subjectType;
        }

        public Association(Stair stair, Exit exit)
        {
            Id = Guid.NewGuid();
            ObjectId = stair.Id;
            ObjectType = stair.GetType().Name;
            SubjectId = exit.Id;
            SubjectType = exit.GetType().Name;
        }

        public Association(Exit exit1, Exit exit2)
        {
            Id = Guid.NewGuid();
            ObjectId = exit1.Id;
            ObjectType = exit1.GetType().Name;
            SubjectId = exit2.Id;
            SubjectType = exit2.GetType().Name;
        }

        public Association(Area area, Exit exit)
        {
            Id = Guid.NewGuid();
            ObjectId = area.Id;
            ObjectType = area.GetType().Name;
            SubjectId = exit.Id;
            SubjectType = exit.GetType().Name;
        }

        public Association(Area area, Stair stair)
        {
            Id = Guid.NewGuid();
            ObjectId = area.Id;
            ObjectType = area.GetType().Name;
            SubjectId = stair.Id;
            SubjectType = stair.GetType().Name;
        }
    }
}
