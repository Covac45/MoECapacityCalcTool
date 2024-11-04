using Microsoft.EntityFrameworkCore;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
{
    //Generic repository for all types

    public interface IGenericRepository<T>
        where T : class
    {
        public IEnumerable<T> GetAll();
        public void AddOrUpdate(T entity);
        public void AddOrUpdateMany(IEnumerable<T> entities);
        public void Remove(T entity);
        public void RemoveMany(IEnumerable<T> entities);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly DbContext DbContext;
        private readonly DbSet<T> _table;

        public GenericRepository(DbContext dbContext) 
        {
            DbContext = dbContext;
            _table = DbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            var table = _table.AsEnumerable();
            return table;
        }

        public void AddOrUpdate(T entity)
        {
            _table.Add(entity);
            DbContext.SaveChanges();
        }

        public void AddOrUpdateMany(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(entity => _table.Add(entity));
            DbContext.SaveChanges();
        }

        public virtual void Remove(T entity)
        {
            _table.Remove(entity);
            DbContext.SaveChanges();
        }

        public virtual void RemoveMany(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(entity => _table.Remove(entity));
            DbContext.SaveChanges();
        }
    }
}
