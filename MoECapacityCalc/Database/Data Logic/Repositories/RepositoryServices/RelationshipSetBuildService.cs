using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Database.Abstractions;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;


namespace MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices
{
    public interface IRelationshipSetBuildService<TEntity>
        where TEntity : MeansOfEscapeEntity<TEntity>
    {
        RelationshipSet<TEntity> GetRelationshipSet(TEntity objectEntity);
    }

    public class RelationshipSetBuildService<TEntity> : IRelationshipSetBuildService<TEntity>
        where TEntity : MeansOfEscapeEntity<TEntity>
    { 
        private readonly DbContext _dbContext;
        private readonly IAssociationsRepository _associationsRepository;

        public RelationshipSetBuildService(DbContext dbContext, IAssociationsRepository associationsRepository)
        {
            _dbContext = dbContext;
            _associationsRepository = associationsRepository;
        }
        public RelationshipSet<TEntity> GetRelationshipSet(TEntity objectEntity)
        {
            var exitRelationships = new RelationshipBuildService<TEntity, Exit>(_dbContext, _associationsRepository).GetRelationships(objectEntity, new Exit());
            var stairRelationships = new RelationshipBuildService<TEntity, Stair>(_dbContext, _associationsRepository).GetRelationships(objectEntity, new Stair());
            var areaRelationships = new RelationshipBuildService<TEntity, Area>(_dbContext, _associationsRepository).GetRelationships(objectEntity, new Area());

            
            return new RelationshipSet<TEntity>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
        }
    }
}
