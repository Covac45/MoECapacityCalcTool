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

        /*public Exit GetById(Guid id)
        {
            var retrievedExit = base.GetById(id);
      
            //retrievedExit.Relationships = _relationshipSetBuilderService.GetRelationshipSet(retrievedExit);

            return retrievedExit;
        }*/

    }

}
