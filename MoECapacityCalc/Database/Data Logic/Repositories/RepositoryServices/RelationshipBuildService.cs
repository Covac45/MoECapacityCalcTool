using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
{

    public class RelationshipBuildService<TEntity1, TEntity2>
        where TEntity1 : MeansOfEscapeEntity<TEntity1>
        where TEntity2 : MeansOfEscapeEntity<TEntity2>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity2> _table;
        private readonly IAssociationsRepository _associationsRepository;

        public RelationshipBuildService(DbContext dbContext, IAssociationsRepository associationsRepository)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity2>();
            _associationsRepository = associationsRepository;
        }

        public List<Relationship<TEntity1, TEntity2>> GetRelationships(TEntity1 objectEntity, TEntity2 subjectEntity)
        {
            var allAssociations = _associationsRepository.GetAllAssociations(objectEntity).ToList();

            var associations = allAssociations.Where(assoc => assoc.SubjectType == subjectEntity.GetType().Name).ToList();

            var entities = associations.Select(assoc => _table.Single(ent => assoc.SubjectId == ent.Id)).ToList();

            var relationships = entities.Select(ent => new Relationship<TEntity1, TEntity2>(objectEntity, ent)).ToList();

            return relationships;
        }
    }
}
