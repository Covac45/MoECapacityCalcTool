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
    public class Association
    {
        public Guid AssociationId;

        public List<Exit>? Exits;

        public List<Stair>? Stairs;

        //public List<Exit> StoreyExits => Exits.Where(exit => exit.ExitType == ExitType.storeyExit).ToList();
        //public List<Exit> FinalExits => Exits.Where(exit => exit.ExitType == ExitType.finalExit).ToList();
        //public List<Exit> AltExits => Exits.Where(exit => exit.ExitType == ExitType.exit).ToList();

        public Association() { }

        public Association(List<Exit>? exits = null,List<Stair>? stairs= null) {

            AssociationId = Guid.NewGuid();
            Exits = exits;
            Stairs = stairs;

        }

        public Association GetAssociations()
        {
            return this;
        }

    }
}
