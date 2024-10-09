using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.UnitTests.TestHelpers;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.UnitTests.TestData
{
    public class TestArea
    {
        public static (List<Exit>, List<Stair>) GetAreaTestData()
        {
            Exit exit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit4 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit5 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit6 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            List<Exit> stair1StoreyExits = new List<Exit> { exit2 };
            List<Exit> stair2StoreyExits = new List<Exit> { exit3 };

            List<Exit> stair1FinalExits = new List<Exit> { exit5 };
            List<Exit> stair2FinalExits = new List<Exit> { exit6 };

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { exit1, exit4 };

            Stair stair1 = new Stair("stair 1", 1000, 3, 0, stair1FinalExits, stair1StoreyExits);
            Stair stair2 = new Stair("stair 2", 1100, 3, 0, stair2FinalExits, stair2StoreyExits);

            List<Stair> stairs = new List<Stair> { stair1, stair2 };

            return (exits, stairs);
        }
    }
}
