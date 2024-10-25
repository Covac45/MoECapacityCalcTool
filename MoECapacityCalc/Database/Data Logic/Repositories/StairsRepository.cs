using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.DomainEntities;


namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class StairsRepository : MeansOfEscapeEntityRepository<Stair>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IRelationshipSetBuildService<Stair> _relationshipSetBuildService;
        private readonly IAssociationsRepository _associationsRepository;


        public StairsRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Stair> relationshipSetBuildService) : base(moEDbContext, relationshipSetBuildService)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuildService = relationshipSetBuildService;
        }
    }
}
