using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Repositories;
using MoECapacityCalc.Database.Repositories.Abstractions;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    internal class AreaRepository : EntityRepository<Area>
    {
        public AreaRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
