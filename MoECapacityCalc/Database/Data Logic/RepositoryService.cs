using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database.Data_Logic
{
    public class RepositoryService
    {
        public IMoEDbContext _moEDbContext { get; }

        public RepositoryService(IMoEDbContext moEDbContext)
        {
            _moEDbContext = moEDbContext;
        }
        public List<Exit> GetExitsFromAssociations(Guid id)
        {
            var relationships = _moEDbContext.Associations
                .Where(rel => rel.ObjectId == id)
                .ToList();

            var exitRelationships = relationships.Where(rel => rel.SubjectType == "Exit").ToList();

            var exits = new List<Exit>();

            exitRelationships.ForEach(exitRel => exits.Add(_moEDbContext.Exits.Single(exit => exitRel.SubjectId == exit.ExitId)));

            return exits;
        }

        public List<Stair> GetStairsFromAssociations(Guid id)
        {
            var relationships = _moEDbContext.Associations
                .Where(rel => rel.ObjectId == id)
                .ToList();

            var stairRelationships = relationships.Where(rel => rel.SubjectType == "Stair").ToList();

            var stairs = new List<Stair>();

            stairRelationships.ForEach(stairRel => stairs.Add(_moEDbContext.Stairs.Single(stair => stairRel.SubjectId == stair.StairId)));

            return stairs;
        }

        public List<Area> GetAreasFromAssociations(Guid id)
        {
            var relationships = _moEDbContext.Associations
                .Where(rel => rel.ObjectId == id)
                .ToList();

            var areaRelationships = relationships.Where(rel => rel.SubjectType == "Area").ToList();

            var areas = new List<Area>();

            areaRelationships.ForEach(areaRel => areas.Add(_moEDbContext.Areas.Single(area => areaRel.SubjectId == area.AreaId)));

            return areas;
        }
    }
}
