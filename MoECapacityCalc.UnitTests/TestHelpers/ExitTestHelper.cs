using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.TestHelpers
{
    internal static class ExitTestHelper
    {
        public static ExitBuilder GetDefaultStoreyExitBuilder()
        {
            return new ExitBuilder("defaultStoreyExit", ExitType.storeyExit, DoorSwing.with, 1050);
        }

        public static ExitBuilder GetDefaultFinalExitBuilder()
        {
            return new ExitBuilder("defaultfinalExit", ExitType.finalExit, DoorSwing.with, 1050);
        }

        public static List<ExitBuilder> GetDefaultExitBuilders()
        {
            var exitBuilders = new List<ExitBuilder>();

            var numberOfDefaultExits = 3;
            for (int i = 0; i < numberOfDefaultExits; i++)
            {
                exitBuilders.Add(GetDefaultStoreyExitBuilder());
                exitBuilders.Add(GetDefaultFinalExitBuilder());
            }

            return exitBuilders;
        }

        public static List<Exit> BuildExits(this List<ExitBuilder> exitBuilders)
        {

            /* foreach (var anExitBuilder in exitBuilders)
             {
                 var exit = anExitBuilder.Build();
                 exits.Add(exit);
             }*/

            var exits = new List<Exit>();
            exitBuilders.ForEach(anExitbuilder => {
                var exit = anExitbuilder.Build();
                exits.Add(exit);
            });

            return exits;
        }

        public static List<Exit> GetDefaultExits()
        {
            var exitBuilders = GetDefaultExitBuilders();


            return exitBuilders.BuildExits();
        }

    }
}
