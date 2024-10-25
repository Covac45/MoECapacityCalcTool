using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
namespace MoECapacityCalc.Database.Repositories.Abstractions
{
    //Generic repository for entity types
    public abstract class EntityRepository<TEntity> : GenericRepository<TEntity> where TEntity : MeansOfEscapeEntity<TEntity>
    {
        private readonly DbContext DbContext;
        private readonly DbSet<TEntity> _table;
        private readonly IRelationshipSetBuildService<TEntity> _relationshipSetBuilderService;

        public EntityRepository(DbContext dbContext, IRelationshipSetBuildService<TEntity> relationshipSetBuilderService) : base(dbContext)
        {
            DbContext = dbContext;
            _table = DbContext.Set<TEntity>();
            _relationshipSetBuilderService = relationshipSetBuilderService;
        }

        public TEntity GetById(Guid id)
        {
             var entity = _table.Single(entity => entity.Id == id);
            entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity);

            return entity;

        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = _table.ToList();

            //TODO add relationships please!

            /*foreach (var entity in entities)
            {
                entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity);
            }*/

            entities.ForEach(entity => entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity));

            entities.ToList();

            return entities;

        }

    }
}
