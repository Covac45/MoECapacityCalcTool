using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Repositories.Abstractions;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityCalc.Database.Data_Logic.Repositories
{
    public class ExitsRepository : EntityRepository<Exit>
    {
        private readonly MoEContext _moEDbContext;
        private readonly IAssociationsRepository _associationsRepository;

        public ExitsRepository(MoEContext moEDbContext, IAssociationsRepository associationsRepository) : base(moEDbContext)
        {
            _moEDbContext = moEDbContext;
            _associationsRepository = associationsRepository;
        }

        public Exit GetExitById(Guid id)
        {
            var retrievedExit = GetById(id);

            var allAssociations = _associationsRepository.GetAllAssociations(retrievedExit).ToList();
            
            retrievedExit.Relationships = new RelationshipSetBuildService<Exit>(_moEDbContext).GetRelationshipSet(retrievedExit, allAssociations);

            /*
            var exitRelationships = new RelationshipBuildService<Exit, Exit>(_moEDbContext).GetRelationships(retrievedExit, allAssociations, new Exit());
            var stairRelationships = new RelationshipBuildService<Exit, Stair>(_moEDbContext).GetRelationships(retrievedExit, allAssociations, new Stair());
            var areaRelationships = new RelationshipBuildService<Exit, Area>(_moEDbContext).GetRelationships(retrievedExit, allAssociations, new Area());

            retrievedExit.Relationships = new RelationshipSet<Exit>
            {   
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
            */
            return retrievedExit;
        }

    }

}
