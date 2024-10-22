using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic;
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
    public class MoEObjectRepository
    {
        public IMoEDbContext _moEDbContext { get; }

        public MoEObjectRepository(IMoEDbContext moEDbContext) 
        {
            _moEDbContext = moEDbContext;
        }

        public Stair GetStairById(Guid id)
        {
            var retrievedStair = _moEDbContext.Stairs.Single(s => s.StairId == id);

            var exits = new RepositoryService(_moEDbContext).GetExitsFromAssociations(id);
            var stairs = new RepositoryService(_moEDbContext).GetStairsFromAssociations(id);

            var exitRelationships = new List<Relationship<Stair, Exit>>();
            var stairRelationships = new List<Relationship<Stair, Stair>>();
            exits.ForEach(exit => exitRelationships.Add(new Relationship<Stair, Exit>(retrievedStair, exit)));
            stairs.ForEach(stair => stairRelationships.Add(new Relationship<Stair, Stair>(retrievedStair, stair)));

            retrievedStair.Relationships = new RelationshipSet<Stair>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships
            };

            return retrievedStair;
        }


        public Exit GetExitById(Guid id)
        {
            var retrievedExit = _moEDbContext.Exits.Single(e => e.ExitId == id);

            var exits = new RepositoryService(_moEDbContext).GetExitsFromAssociations(id);
            var stairs = new RepositoryService(_moEDbContext).GetStairsFromAssociations(id);

            var exitRelationships = new List<Relationship<Exit, Exit>>();
            var stairRelationships = new List<Relationship<Exit, Stair>>();
            exits.ForEach(exit => exitRelationships.Add(new Relationship<Exit, Exit>(retrievedExit, exit)));
            stairs.ForEach(stair => stairRelationships.Add(new Relationship<Exit, Stair>(retrievedExit, stair)));

            retrievedExit.Relationships = new RelationshipSet<Exit>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships
            };

            return retrievedExit;
        }

        public Area GetAreaById(Guid id)
        {
            var retrievedArea = _moEDbContext.Areas.Single(e => e.AreaId == id);

            var exits = new RepositoryService(_moEDbContext).GetExitsFromAssociations(id);
            var stairs = new RepositoryService(_moEDbContext).GetStairsFromAssociations(id);

            var exitRelationships = new List<Relationship<Area, Exit>>();
            var stairRelationships = new List<Relationship<Area, Stair>>();
            exits.ForEach(exit => exitRelationships.Add(new Relationship<Area, Exit>(retrievedArea, exit)));
            stairs.ForEach(stair => stairRelationships.Add(new Relationship<Area, Stair>(retrievedArea, stair)));

            retrievedArea.Relationships = new RelationshipSet<Area>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships
            };

            return retrievedArea;
        }

    }
}
