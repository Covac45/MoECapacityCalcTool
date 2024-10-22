using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database
{
    public class AssociationsDictionary
    {
        /*public MoEContext Context { get; set; }
        public AssociationsDictionary(MoEContext context)
        {
            Context = context;
        }

        public Dictionary<Guid, Exit> CreateExitDictionary()
        {
            Dictionary<Guid, Exit> exitDictionary = new();

            foreach (var exit in Context.Exits)
            {
                exitDictionary.Add(exit.ExitId, exit);
            }
            
            return exitDictionary;
        }
        public Dictionary<Guid, Stair> CreateStairDictionary()
        {
            Dictionary<Guid, Stair> stairDictionary = new();

            foreach (var stair in Context.Stairs)
            {
                stairDictionary.Add(stair.StairId, stair);
            }
            
            return stairDictionary;
        }*/


    }
}
