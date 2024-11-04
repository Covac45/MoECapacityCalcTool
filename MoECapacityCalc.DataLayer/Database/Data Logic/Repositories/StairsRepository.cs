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


        public StairsRepository(MoEContext moEDbContext,
                                    IRelationshipSetBuildService<Stair> relationshipSetBuildService,
                                    IAssociationsRepository associationsRepository)
                                    : base(moEDbContext, relationshipSetBuildService, associationsRepository)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuildService = relationshipSetBuildService;
            _associationsRepository = associationsRepository;
        }

        public void AddOrUpdate(Stair stair)
        {
            var retrievedStair = _moEDbContext.Stairs.SingleOrDefault(e => e.Id == stair.Id);

            if (retrievedStair == null)
            {
                base.AddOrUpdate(stair);
            }
            else
            {
                retrievedStair.Name = stair.Name;
                retrievedStair.StairWidth = stair.StairWidth;
                retrievedStair.FloorsServed = stair.FloorsServed;
                retrievedStair.FinalExitLevel = stair.FinalExitLevel;
                retrievedStair.Relationships = stair.Relationships;

                _moEDbContext.SaveChanges();
            }
        }

    }
}
