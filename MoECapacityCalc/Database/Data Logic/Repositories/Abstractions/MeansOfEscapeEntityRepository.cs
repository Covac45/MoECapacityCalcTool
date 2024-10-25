using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
namespace MoECapacityCalc.Database.Repositories.Abstractions
{
    //Generic repository for entity types
    public interface IMeansofEscapeEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : Entity
    {
        
    }

    public abstract class MeansOfEscapeEntityRepository<TEntity> : EntityRepository<TEntity>
        where TEntity : MeansOfEscapeEntity<TEntity>
    {
        private readonly DbContext DbContext;
        private readonly DbSet<TEntity> _table;
        private readonly IRelationshipSetBuildService<TEntity> _relationshipSetBuilderService;
        private readonly IAssociationsRepository _associationsRepository;

        public MeansOfEscapeEntityRepository(DbContext dbContext, IRelationshipSetBuildService<TEntity> relationshipSetBuilderService, IAssociationsRepository associationsRepository) : base(dbContext)
        {
            DbContext = dbContext;
            _table = DbContext.Set<TEntity>();
            _relationshipSetBuilderService = relationshipSetBuilderService;
            _associationsRepository = associationsRepository;
        }

        public override TEntity GetById(Guid id)
        {
            var entity = base.GetById(id);
            entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity);

            return entity;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            var entities = base.GetAll().ToList();

            entities.ForEach(entity => entity.Relationships = _relationshipSetBuilderService.GetRelationshipSet(entity));

            entities.ToList();

            return entities;
        }

        public override void Remove(TEntity entity)
        {
            var objectAssociations = _associationsRepository.GetAllAssociationsForObject(entity).ToList();
            var subjectAssociations = _associationsRepository.GetAllAssociationsForSubject(entity).ToList();
            _associationsRepository.RemoveMany(objectAssociations);
            _associationsRepository.RemoveMany(subjectAssociations);

            _table.Remove(entity);
            DbContext.SaveChanges();
        }

        public override void RemoveMany(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(entity => Remove(entity));
            DbContext.SaveChanges();
        }

    }
}
