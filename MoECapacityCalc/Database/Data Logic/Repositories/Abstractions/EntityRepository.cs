using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Interfaces;

namespace MoECapacityCalc.Database.Repositories.Abstractions
{
    public abstract class EntityRepository<TEntity> : GenericRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext DbContext;
        private readonly DbSet<TEntity> _table;

        public EntityRepository(DbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext;
            _table = DbContext.Set<TEntity>();
        }

        public TEntity GetById(Guid id)
        {
            return _table.Single(entity => entity.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _table.AsEnumerable();
        }
    }
}
