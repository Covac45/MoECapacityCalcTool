using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    internal class ExitsRepository : EntityRepository<Exit>
    {
        public ExitsRepository(DbContext moEDbContext) : base(moEDbContext)
        {
        }
    }
}
