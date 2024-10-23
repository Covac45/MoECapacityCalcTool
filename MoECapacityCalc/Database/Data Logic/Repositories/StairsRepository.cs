using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class StairsRepository : EntityRepository<Stair>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IRelationshipSetBuildService<Stair> _relationshipSetBuildService;
        private readonly IAssociationsRepository _associationsRepository;


        public StairsRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Stair> relationshipSetBuildService) : base(moEDbContext, relationshipSetBuildService)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuildService = relationshipSetBuildService;
        }

        /*public Stair GetStairById(Guid id)
        {
            var retrievedStair = GetById(id);

            var allAssociations = _associationsRepository.GetAllAssociations(retrievedStair).ToList();

            var exitRelationships = new RelationshipBuildService<Stair, Exit>(_moEDbContext).GetRelationships(retrievedStair, allAssociations, new Exit());
            var stairRelationships = new RelationshipBuildService<Stair, Stair>(_moEDbContext).GetRelationships(retrievedStair, allAssociations, new Stair());
            var areaRelationships = new RelationshipBuildService<Stair, Area>(_moEDbContext).GetRelationships(retrievedStair, allAssociations, new Area());

            retrievedStair.Relationships = new RelationshipSet<Stair>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
            return retrievedStair;
        }*/

    }
}
