using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Utilities.Associations
{
    public class Associations
    {
        public List<Exit>? Exits;

        public List<Stair>? Stairs;

        public List<Exit> StoreyExits => Exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
        public List<Exit> FinalExits => Exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
        public List<Exit> AltExits => Exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

        public Associations(List<Exit>? exits = null,List<Stair>? stairs= null) {

            Exits = exits;
            Stairs = stairs;

        }

        public Associations GetAssociations()
        {
            return this;
        }

    }
}
