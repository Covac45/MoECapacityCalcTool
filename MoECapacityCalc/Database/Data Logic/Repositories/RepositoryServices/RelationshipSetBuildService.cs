using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Interfaces;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;


namespace MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices
{
    public class RelationshipSetBuildService<TEntity>
        where TEntity : Entity
    { 
        private readonly DbContext _dbContext;


        public RelationshipSetBuildService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public RelationshipSet<TEntity> GetRelationshipSet(TEntity objectEntity, List<Association> allAssociations)
        {


            var exitRelationships = new RelationshipBuildService<TEntity, Exit>(_dbContext).GetRelationships(objectEntity, allAssociations, new Exit());
            var stairRelationships = new RelationshipBuildService<TEntity, Stair>(_dbContext).GetRelationships(objectEntity, allAssociations, new Stair());
            var areaRelationships = new RelationshipBuildService<TEntity, Area>(_dbContext).GetRelationships(objectEntity, allAssociations, new Area());

            
            return new RelationshipSet<TEntity>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
        }
    }
}
