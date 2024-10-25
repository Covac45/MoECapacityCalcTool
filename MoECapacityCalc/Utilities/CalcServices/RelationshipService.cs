using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Services
{
    public class RelationshipService
    {
        public RelationshipService() { }


        public Object GetRelationshipSubject(Association relationship)
        {
            Association Relationship = relationship;

            using MoEContext context = new();

            if (Relationship.SubjectType == "Exit")
            {
                return context.Exits.FirstOrDefault(e => e.Id == Relationship.SubjectId);
            }
            else if (Relationship.SubjectType == "Stair")
            {
                return context.Stairs.FirstOrDefault(s => s.Id == Relationship.SubjectId);
            }
            else
            {
                return null;
            }
        }

        public List<Exit> GetExitsRelatedToStair(Stair stair)
        {
            Stair Stair = stair;

            using MoEContext context = new();

            var exitRelationships = context.Associations.Include(e => e.ObjectType == "Exit").Where(s => s.SubjectId == Stair.Id).ToList();

            List<Exit> exits = new List<Exit>() { };

            foreach (var exitRelationship in exitRelationships)
            {
                exits.Add((Exit)context.Exits.Where(e => e.Id == exitRelationship.SubjectId));
            }

            return exits;

        }

    }
}
