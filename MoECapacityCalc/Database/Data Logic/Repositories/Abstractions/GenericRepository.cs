using Microsoft.EntityFrameworkCore;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _table;

        public GenericRepository(DbContext dbContext) 
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.AsEnumerable();
        }
    }
}
