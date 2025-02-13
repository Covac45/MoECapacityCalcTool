using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.DomainEntities;
namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class AreasRepository : MeansOfEscapeEntityRepository<Area>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IRelationshipSetBuildService<Area> _relationshipSetBuildService;
        private readonly IAssociationsRepository _associationsRepository;
        public AreasRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Area> relationshipSetBuildService, IAssociationsRepository associationsRepository)
                              : base(moEDbContext, relationshipSetBuildService, associationsRepository)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuildService = relationshipSetBuildService;
            _associationsRepository = associationsRepository;
        }

        public void AddOrUpdate(Area area)
        {
            var retrievedArea = _moEDbContext.Areas.SingleOrDefault(e => e.Id == area.Id);

            if (retrievedArea == null)
            {
                base.AddOrUpdate(area);
            }
            else
            {
                retrievedArea.Name = area.Name;
                retrievedArea.FloorLevel = area.FloorLevel;
                retrievedArea.Relationships = area.Relationships;

                _moEDbContext.SaveChanges();
            }
        }
    }
}
