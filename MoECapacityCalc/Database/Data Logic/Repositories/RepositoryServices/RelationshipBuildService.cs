using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Interfaces;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions
{
    /*public interface IRelationshipBuildService<TEntity> where TEntity : Entity
    {
        public List<TEntity> GetAssociatedEntities(List<Association> allAssociations, Entity entity);
    }*/

    public class RelationshipBuildService<TEntity>() where TEntity : Entity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _table;

        public RelationshipBuildService(DbContext dbContext) : this()
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }

        public List<TEntity> GetAssociatedEntities(List<Association> allAssociations, Entity entity)
        {
            var associations = allAssociations.Where(assoc => assoc.SubjectType == entity.GetType().Name).ToList();

            var entities = associations.Select(assoc => _table.Single(ent => assoc.SubjectId == ent.Id)).ToList();

            return entities;
        }



    }
}
