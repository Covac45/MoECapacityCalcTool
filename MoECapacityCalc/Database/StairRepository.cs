using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database
{
    internal class StairRepository
    {
        public IMoEDbContext _moEDbContext { get; }

        public StairRepository(IMoEDbContext moEDbContext) 
        {
            _moEDbContext = moEDbContext;
        }

        public Stair GetStairById(Guid id)
        {
            var retrievedStair = _moEDbContext.Stairs.Single(s => s.StairId == id);
            var relationships = _moEDbContext.Relationships
                .Where(rel => rel.ObjectId == retrievedStair.StairId)
                .ToList();

            var exitRelationships = relationships.Where(rel => rel.SubjectType == "Exit").ToList();
            var stairRelationships = relationships.Where(rel => rel.SubjectType == "Stair").ToList();

            var exits = new List<Exit>();
            var stairs = new List<Stair>();

            exitRelationships.ForEach(exitRel => exits.Add(_moEDbContext.Exits.Single(exit => exitRel.SubjectId == exit.ExitId)));
            stairRelationships.ForEach(stairRel => stairs.Add(_moEDbContext.Stairs.Single(stair => stairRel.SubjectId == stair.StairId)));

            var castedExitRelationships = new List<Association<Stair, Exit>>();
            var castedStairRelationships = new List<Association<Stair, Stair>>();
            exits.ForEach(exit => castedExitRelationships.Add(new Association<Stair, Exit>(retrievedStair, exit)));
            stairs.ForEach(stair => castedStairRelationships.Add(new Association<Stair, Stair>(retrievedStair, stair)));

            retrievedStair.Relationships = new RelationshipSet<Stair>
            {
                ExitRelationships = castedExitRelationships,
                StairRelationships = castedStairRelationships
            };

            return retrievedStair;
        }

    }
}
