using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.UnitTests.TestHelpers;
using MoECapacityCalc.Utilities.Associations;
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
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);

            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            //Exits associated with stairs
            List<Exit> stair1Exits = new List<Exit> { storeyExit1, finalExit1 };
            List<Exit> stair2Exits = new List<Exit> { storeyExit2, finalExit3 };

            //exits not associated with stairs
            List<Exit> exits = new List<Exit> { storeyExit3, finalExit3 };

            Associations stair1Associations = new Associations(stair1Exits);
            Associations stair2Associations = new Associations(stair2Exits);

            Stair stair1 = new Stair("stair 1", 1000, 3, 0, stair1Associations);
            Stair stair2 = new Stair("stair 2", 1100, 3, 0, stair2Associations);

            List<Stair> stairs = new List<Stair> { stair1, stair2 };

            return (exits, stairs);
        }
    }
}
