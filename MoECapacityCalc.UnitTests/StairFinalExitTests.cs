using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Stairs.StairFinalExits;
using MoECapacityCalc.Utilities.Datastructs;

namespace MoECapacityCalc.UnitTests
{
    public class StairFinalExitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(900, 1200, 0)]
        [TestCase(1200, 1200, 60)]
        [TestCase(1200, 1500, 15)]
        [TestCase(1050, 1050, 52.5)]
        public void MergingFlowCapacityTest(double exitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, exitWidth);
            List<Exit> finalExits = new List<Exit>() { finalExit1 };

            Stair stair1 = new Stair("stair 1", stairWidth, 1, 0, finalExits);
            //List<Stair> stairs = new List<Stair>() { stair1 };

            StairFinalExit finalExitLevel = new StairFinalExit(stair1, finalExits);

            double exitCapacity = finalExitLevel.CalcMergingFlowCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        [TestCase(1200, 850, 1200, 60)]
        [TestCase(1500, 850, 1200, 110)]
        [TestCase(1500, 1050, 1200, 120)]
        [TestCase(1500, 1050, 1500, 75)]
        public void StairFinalExitLevelCapacityTests(double finalExitWidth, double storeyExitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, finalExitWidth);
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, storeyExitWidth);

            List<Exit> finalExits = new List<Exit>() { finalExit1 };
            List<Exit> storeyExits = new List<Exit>() { storeyExit1 };

            Stair stair1 = new Stair("stair 1", stairWidth, 1, 0, finalExits, storeyExits);
            //List<Stair> stairs = new List<Stair>() { stair1 };


            StairFinalExit finalExitLevel = new StairFinalExit(stair1, storeyExits);

            double exitCapacity = stair1.CalcFinalExitLevelCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
