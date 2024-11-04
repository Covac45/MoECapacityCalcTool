using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.DomainEntities.Abstractions;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
{
    public interface IEntityRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : Entity
    {
        public TEntity GetById(Guid id);

    }

    public abstract class EntityRepository<TEntity> : GenericRepository<TEntity>, IEntityRepository<TEntity>
        where TEntity : Entity 
    {
        private readonly DbContext DbContext;
        private readonly DbSet<TEntity> _table;

        public EntityRepository(DbContext dbContext) : base(dbContext) 
        {
            DbContext = dbContext;
            _table = DbContext.Set<TEntity>();
        }

        public virtual TEntity GetById(Guid id)
        {
            var entity = _table.Single(entity => entity.Id == id);

            return entity;
        }

    }
}
