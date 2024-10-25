using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public interface IAssociationsRepository
    {
        public IEnumerable<Association> GetAllAssociations(Entity entity);
    }

    public class AssociationsRepository : EntityRepository<Association>, IAssociationsRepository
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
