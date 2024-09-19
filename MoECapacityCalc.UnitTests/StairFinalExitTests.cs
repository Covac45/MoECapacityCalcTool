using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Stairs.StairFinalExits;

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
            Exit finalExit = new Exit(ExitType.finalExit, DoorSwing.with, exitWidth);
            Stair stair = new Stair(stairWidth, 1, 0, finalExit);

            StairFinalExit finalExitLevel = new StairFinalExit(stair, finalExit);

            double exitCapacity = finalExitLevel.CalcMergingFlowCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        [TestCase(1200, 850, 1200, 60)]
        [TestCase(1500, 850, 1200, 110)]
        [TestCase(1500, 1050, 1200, 120)]
        [TestCase(1500, 1050, 1500, 75)]
        public void StairFinalExitLevelCapacityTests(double finalExitWidth, double storeyExitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit finalExit = new Exit(ExitType.finalExit, DoorSwing.with, finalExitWidth);
            Exit storeyExit = new Exit(ExitType.storeyExit, DoorSwing.with, storeyExitWidth);
            
            Stair stair = new Stair(stairWidth, 1, 0, finalExit, storeyExit);

            StairFinalExit finalExitLevel = new StairFinalExit(stair, storeyExit);

            double exitCapacity = finalExitLevel.CalcFinalExitLevelCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
