using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Stairs;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class StairsRepository : EntityRepository<Stair>
    {
        public StairsRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
