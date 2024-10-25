using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class ExitsRepository : MeansOfEscapeEntityRepository<Exit>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IRelationshipSetBuildService<Exit> _relationshipSetBuildService;
        private readonly IAssociationsRepository _associationsRepository;

        public ExitsRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Exit> relationshipSetBuilderService, IAssociationsRepository associationsRepository) : base(moEDbContext, relationshipSetBuilderService, associationsRepository)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuildService = relationshipSetBuilderService;
            _associationsRepository = associationsRepository;
        }

    }

}
