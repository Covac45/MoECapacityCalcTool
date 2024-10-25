using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
{
    //Generic repository for all types
    public abstract class GenericRepository<T> where T : class
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
    }
}
