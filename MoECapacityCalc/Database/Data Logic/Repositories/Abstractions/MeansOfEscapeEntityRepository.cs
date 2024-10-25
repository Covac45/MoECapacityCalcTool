using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
namespace MoECapacityCalc.Database.Repositories.Abstractions
{
    //Generic repository for entity types
    public abstract class MeansOfEscapeEntityRepository<TEntity> : EntityRepository<TEntity> where TEntity : MeansOfEscapeEntity<TEntity>
    {
        private readonly DbContext DbContext;
        private readonly DbSet<TEntity> _table;
        private readonly IRelationshipSetBuildService<TEntity> _relationshipSetBuilderService;

        public MeansOfEscapeEntityRepository(DbContext dbContext, IRelationshipSetBuildService<TEntity> relationshipSetBuilderService) : base(dbContext)
        {
            DbContext = dbContext;
            _table = DbContext.Set<TEntity>();
            _relationshipSetBuilderService = relationshipSetBuilderService;
        }

        public override TEntity GetById(Guid id)
        {
            var entity = base.GetById(id);
            entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity);

            return entity;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            var entities = base.GetAll().ToList();

            entities.ForEach(entity => entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity));

            entities.ToList();

            return entities;

        }
        /*public void Add(TEntity entity)
        {
            _table.Add(entity);
            DbContext.SaveChanges();
        }

        public void AddMany(List<TEntity> entities)
        {
            entities.ForEach(entity => _table.Add(entity));
            DbContext.SaveChanges();
        }*/

    }
}
