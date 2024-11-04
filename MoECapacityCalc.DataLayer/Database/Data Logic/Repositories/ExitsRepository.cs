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

        public void AddOrUpdate(Exit exit)
        {
            var retrievedExit = _moEDbContext.Exits.SingleOrDefault(e => e.Id == exit.Id);

            if (retrievedExit == null)
            {
                base.AddOrUpdate(exit);
            }
            else
            {
                retrievedExit.Name = exit.Name;
                retrievedExit.ExitType = exit.ExitType;
                retrievedExit.ExitWidth = exit.ExitWidth;
                retrievedExit.DoorSwing = exit.DoorSwing;
                retrievedExit.Relationships = exit.Relationships;

                _moEDbContext.SaveChanges();
            }
        }

        public void AddOrUpdateMany(List<Exit> exits)
        {
            foreach (var exit in exits)
            {
                base.AddOrUpdate(exit);

            }
        }
    }
}
