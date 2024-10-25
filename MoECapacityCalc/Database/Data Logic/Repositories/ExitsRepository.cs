using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;


namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class ExitsRepository : EntityRepository<Exit>
    {
        private readonly MoEContext _moEDbContext;

        public ExitsRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Exit> relationshipSetBuilderService) : base(moEDbContext, relationshipSetBuilderService)
        {
            _moEDbContext = moEDbContext;
        }

    }

}
