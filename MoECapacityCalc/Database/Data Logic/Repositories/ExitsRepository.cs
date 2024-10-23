using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;


namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class ExitsRepository : EntityRepository<Exit>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IRelationshipSetBuildService<Exit> _relationshipSetBuilderService;


        public ExitsRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Exit> relationshipSetBuilderService) : base(moEDbContext)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuilderService = relationshipSetBuilderService;
        }

        public Exit GetById(Guid id)
        {
            var retrievedExit = GetById(id);
      
            retrievedExit.Relationships = _relationshipSetBuilderService.GetRelationshipSet(retrievedExit);

            return retrievedExit;
        }

    }

}
