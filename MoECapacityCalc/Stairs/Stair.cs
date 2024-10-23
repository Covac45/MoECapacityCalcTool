using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Services;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Database.Interfaces;

namespace MoECapacityCalc.Stairs
{
    public class Stair : Entity
    {
        public double StairWidth { get; set; }
        public int FloorsServed { get; set; }
        public int FinalExitLevel { get; set; }
        public RelationshipSet<Stair> Relationships { get; set; }

        public Stair() { }

        public Stair(string name, double width, int floorsServed, int finalExitLevel)
        {
            Id = Guid.NewGuid();
            Name = name;
            StairWidth = width;
            FloorsServed = floorsServed;
            FinalExitLevel = finalExitLevel;
            Relationships = new RelationshipSet<Stair>();
        }

    }
}
