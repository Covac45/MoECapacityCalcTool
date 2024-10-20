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

namespace MoECapacityCalc.Stairs
{
    public class Stair
    {
        public Guid StairId;
        public string StairName;
        public double StairWidth;
        public int FloorsServed;
        public int FinalExitLevel;
        public List<Exit> FinalExits;
        public List<Exit> StoreyExits;

        public Stair() { }

        public Stair(string name, double width, int floorsServed, int finalExitLevel, Association? associations = null)
        {
            StairId = Guid.NewGuid();
            StairName = name;
            StairWidth = width;
            FloorsServed = floorsServed;
            FinalExitLevel = finalExitLevel;
            //FinalExits = associations.FinalExits;
            //StoreyExits = associations.StoreyExits;
        }

    }
}
