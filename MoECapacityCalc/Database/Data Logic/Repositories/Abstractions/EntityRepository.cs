using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
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

        public virtual TEntity GetById(Guid id)
        {
            var entity = _table.Single(entity => entity.Id == id);

            return entity;
        }

        public void Add(TEntity entity)
        {
            _table.Add(entity);
            DbContext.SaveChanges();
        }

        public void AddMany(List<TEntity> entities)
        {
            entities.ForEach(entity => _table.Add(entity));
            DbContext.SaveChanges();
        }
    }
}
