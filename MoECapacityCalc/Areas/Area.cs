using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Areas
{
    public class Area
    {
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }
        public int FloorLevel { get; set; }

        public RelationshipSet<Area> Relationships { get; set; }

        public Area() { }

        public Area(int floorLevel, string areaName)
        {
            AreaId = Guid.NewGuid();
            AreaName = areaName;
            FloorLevel = floorLevel;
            Relationships = new RelationshipSet<Area>();
        }

    }
}
