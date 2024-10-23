using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Interfaces;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public interface IAssociationsRepository
    {
        public IEnumerable<Association> GetAllAssociations(Entity entity);
    }

    public class AssociationsRepository : GenericRepository<Association>, IAssociationsRepository
    {
        private readonly MoEContext _moEDbContext;

        public AssociationsRepository(MoEContext moEContext) : base(moEContext)
        {
            _moEDbContext = moEContext;
        }
        public IEnumerable<Association> GetAllAssociations(Entity entity)
        {
            var associations = GetAll().Where(a => a.ObjectId == entity.Id);
            return associations;

        }
    }
}
