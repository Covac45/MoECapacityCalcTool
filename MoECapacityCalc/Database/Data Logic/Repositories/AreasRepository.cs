using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Repositories;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class AreasRepository : EntityRepository<Area>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IAssociationsRepository _associationsRepository;
        public AreasRepository(MoEContext moEDbContext, IAssociationsRepository associationsRepository) : base(moEDbContext)
        {
            _moEDbContext = moEDbContext;
            _associationsRepository = associationsRepository;
        }
        public Area GetAreaById(Guid id)
        {
            var retrievedArea = GetById(id);

            var allAssociations = _associationsRepository.GetAllAssociations(retrievedArea).ToList();

            var exits = new RelationshipBuildService<Exit>(_moEDbContext).GetAssociatedEntities(allAssociations, new Exit());
            var stairs = new RelationshipBuildService<Stair>(_moEDbContext).GetAssociatedEntities(allAssociations, new Stair());
            var areas = new RelationshipBuildService<Area>(_moEDbContext).GetAssociatedEntities(allAssociations, new Area());


            var exitRelationships = exits.Select(exit => new Relationship<Area, Exit>(retrievedArea, exit)).ToList();
            var stairRelationships = stairs.Select(stair => new Relationship<Area, Stair>(retrievedArea, stair)).ToList();
            var areaRelationships = areas.Select(area => new Relationship<Area, Area>(retrievedArea, area)).ToList();


            retrievedArea.Relationships = new RelationshipSet<Area>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
            return retrievedArea;
        }


    }
}
