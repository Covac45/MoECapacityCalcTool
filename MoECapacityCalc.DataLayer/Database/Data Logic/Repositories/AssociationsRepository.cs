using MoECapacityCalc.DomainEntities.Abstractions;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public interface IAssociationsRepository : IGenericRepository<Association>
    {
        public IEnumerable<Association> GetAllAssociationsForObject(Entity entity);
        public IEnumerable<Association> GetAllAssociationsForSubject(Entity entity);
    }

    public class AssociationsRepository : GenericRepository<Association>, IAssociationsRepository
    {
        private readonly MoEContext _moEDbContext;

        public AssociationsRepository(MoEContext moEContext) : base(moEContext)
        {
            _moEDbContext = moEContext;
        }
        public IEnumerable<Association> GetAllAssociationsForObject(Entity entity)
        {
            var associations = GetAll().Where(a => a.ObjectId == entity.Id);
            return associations;
        }

        public IEnumerable<Association> GetAllAssociationsForSubject(Entity entity)
        {
            var associations = GetAll().Where(a => a.SubjectId == entity.Id);
            return associations;

        }

        public void AddOrUpdate(Association association)
        {
            var retrievedAssociation = _moEDbContext.Associations.SingleOrDefault(a => a.Id == association.Id);

            if (retrievedAssociation == null)
            {
                base.AddOrUpdate(association);
            }
            else
            {
                retrievedAssociation.ObjectId = association.ObjectId;
                retrievedAssociation.ObjectType = association.ObjectType;
                retrievedAssociation.SubjectId = association.SubjectId;
                retrievedAssociation.SubjectType = association.SubjectType;

                _moEDbContext.SaveChanges();
            }
        }

    }
}
