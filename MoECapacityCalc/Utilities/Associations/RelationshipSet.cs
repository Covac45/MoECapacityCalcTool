using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Associations
{
    public class RelationshipSet<T1>
    {
        public List<Association<T1, Exit>> ExitRelationships { get; set; }
        public List<Association<T1, Stair>> StairRelationships { get; set; }

        public List<Exit> GetExits() => ExitRelationships.Select(rel => rel.Object2).ToList();
        public List<Stair> GetStairs() => StairRelationships.Select(rel => rel.Object2).ToList();
    }
}
