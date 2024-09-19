using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Areas;


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
            Exit exit1 = new Exit(ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit2 = new Exit(ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit3 = new Exit(ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit4 = new Exit(ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit5 = new Exit(ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit6 = new Exit(ExitType.finalExit, DoorSwing.with, 1050);

            List<Exit> storeyExits = new List<Exit> { exit1 };

            List<Exit> finalExits = new List<Exit> { exit4 };

            Stair stair1 = new Stair(1000, 3, 0, exit5, exit2);
            Stair stair2 = new Stair(1100, 3, 0, exit6, exit3);

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
