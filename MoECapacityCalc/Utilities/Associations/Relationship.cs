using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Relationship
    {
        public Guid RelationshipId { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectType { get; set; }

        public Relationship() { }

        public Relationship(Guid objectId, string objectType, Guid subjectId, string subjectType)
        {
            RelationshipId = Guid.NewGuid();
            ObjectId = objectId;
            ObjectType = objectType;
            SubjectId = subjectId;
            SubjectType = subjectType;
        }

        public Relationship(Stair stair, Exit exit)
        {
            RelationshipId = Guid.NewGuid();
            ObjectId = stair.StairId;
            ObjectType = stair.GetType().Name;
            SubjectId = exit.ExitId;
            SubjectType = exit.GetType().Name;
        }

        public Relationship(Exit exit1, Exit exit2)
        {
            RelationshipId = Guid.NewGuid();
            ObjectId = exit1.ExitId;
            ObjectType = exit2.GetType().Name;
            SubjectId = exit2.ExitId;
            SubjectType = exit2.GetType().Name;
        }
    }
}
