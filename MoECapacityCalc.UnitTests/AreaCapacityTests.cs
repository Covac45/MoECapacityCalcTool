using MoECapacityCalc.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Utilities.Datastructs;


namespace MoECapacityCalc.UnitTests
{
    public class AreaCapacityTests
    {

        [SetUp]

        public void Setup()
        {
        }

        public (List<Exit>, List<Exit>, List<Stair>) InitialiseLists()
        {
            Exit exit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit4 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit5 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit6 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            List<Exit> storeyExits = new List<Exit> { exit1 };
            List<Exit> stair1StoreyExits = new List<Exit> { exit2 };
            List<Exit> stair2StoreyExits = new List<Exit> { exit3 };

            List<Exit> finalExits = new List<Exit> { exit4 };
            List<Exit> stair1FinalExits = new List<Exit> { exit5 };
            List<Exit> stair2FinalExits = new List<Exit> { exit6 };



            Stair stair1 = new Stair("stair 1", 1000, 3, 0, stair1FinalExits, stair1StoreyExits);
            Stair stair2 = new Stair("stair 2", 1100, 3, 0, stair2FinalExits, stair2StoreyExits);

            List<Stair> stairs = new List<Stair> { stair1, stair2 };

            return (storeyExits, finalExits, stairs);
        }


        [TestCase(325)]
        public void AreaFinalExitLevelCapacityTest(double expectedExitCapacity)
        {

            var (storeyExits, finalExits, stairs) = InitialiseLists();

            Area area1 = new Area(0, storeyExits, finalExits, stairs);

            double exitCapacity = area1.CalcDiscountedExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
