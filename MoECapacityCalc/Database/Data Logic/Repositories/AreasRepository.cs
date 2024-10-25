using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class AreasRepository : EntityRepository<Area>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IRelationshipSetBuildService<Area> _relationshipSetBuildService;
        public AreasRepository(MoEContext moEDbContext, IRelationshipSetBuildService<Area> relationshipSetBuildService) : base(moEDbContext, relationshipSetBuildService)
        {
            _moEDbContext = moEDbContext;
            _relationshipSetBuildService = relationshipSetBuildService;
        }

    }
}
