using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class StairsRepository : EntityRepository<Stair>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IAssociationsRepository _associationsRepository;


        public StairsRepository(MoEContext moEDbContext, IAssociationsRepository associationsRepository) : base(moEDbContext)
        {
            _moEDbContext = moEDbContext;
            _associationsRepository = associationsRepository;
        }

        public Stair GetStairById(Guid id)
        {
            var retrievedStair = GetById(id);

            var allAssociations = _associationsRepository.GetAllAssociations(retrievedStair).ToList();

            var exits = new RelationshipBuildService<Exit>(_moEDbContext).GetAssociatedEntities(allAssociations, new Exit());
            var stairs = new RelationshipBuildService<Stair>(_moEDbContext).GetAssociatedEntities(allAssociations, new Stair());
            var areas = new RelationshipBuildService<Area>(_moEDbContext).GetAssociatedEntities(allAssociations, new Area());

            var exitRelationships = exits.Select(exit => new Relationship<Stair, Exit>(retrievedStair, exit)).ToList();
            var stairRelationships = stairs.Select(stair => new Relationship<Stair, Stair>(retrievedStair, stair)).ToList();
            var areaRelationships = areas.Select(area => new Relationship<Stair, Area>(retrievedStair, area)).ToList();

            retrievedStair.Relationships = new RelationshipSet<Stair>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
            return retrievedStair;
        }

    }
}
