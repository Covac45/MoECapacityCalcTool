using MoECapacityCalc.DomainEntities;

namespace MoECapacityCalc.Utilities.Associations
{
    public class RelationshipSet<T1>
    {
        public List<Relationship<T1, Exit>> ExitRelationships { get; set; } = new List<Relationship<T1, Exit>>();
        public List<Relationship<T1, Stair>> StairRelationships { get; set; } = new List<Relationship<T1, Stair>>();

        public List<Relationship<T1, Area>> AreaRelationships { get; set; } = new List<Relationship<T1, Area>>();

        public List<Exit> GetExits() => ExitRelationships.Select(rel => rel.Object2).ToList();
        public List<Stair> GetStairs() => StairRelationships.Select(rel => rel.Object2).ToList();
        public List<Area> GetAreas() => AreaRelationships.Select(rel => rel.Object2).ToList();
    }
}
