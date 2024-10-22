using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    internal class AssociationsRepository : GenericRepository<Association>
    {
        public AssociationsRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
