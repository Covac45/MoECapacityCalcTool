using Microsoft.EntityFrameworkCore;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.Abstractions;
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
        private readonly IAssociationsRepository _associationsRepository;
        public AreasRepository(MoEContext moEDbContext, IAssociationsRepository associationsRepository) : base(moEDbContext)
        {
            _moEDbContext = moEDbContext;
            _associationsRepository = associationsRepository;
        }
        public Area GetAreaById(Guid id)
        {
            var retrievedArea = GetById(id);

            var allAssociations = _associationsRepository.GetAllAssociations(retrievedArea).ToList();

            var exitRelationships = new RelationshipBuildService<Area, Exit>(_moEDbContext).GetRelationships(retrievedArea, allAssociations, new Exit());
            var stairRelationships = new RelationshipBuildService<Area, Stair>(_moEDbContext).GetRelationships(retrievedArea, allAssociations, new Stair());
            var areaRelationships = new RelationshipBuildService<Area, Area>(_moEDbContext).GetRelationships(retrievedArea, allAssociations, new Area());

            retrievedArea.Relationships = new RelationshipSet<Area>
            {
                ExitRelationships = exitRelationships,
                StairRelationships = stairRelationships,
                AreaRelationships = areaRelationships
            };
            return retrievedArea;
        }


    }
}
