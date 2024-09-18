using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.FinalExitLevel;

namespace MoECapacityCalc.UnitTests
{
    public class FinalExitLevelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1200, 1200, 60)]
        [TestCase(900, 1200, 0)]
        public void MergingFlowCapacityTest(double exitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit finalExit = new Exit(ExitType.finalExit, DoorSwing.with, exitWidth);
            Stair stair = new Stair(stairWidth, 1);

            FinalExitLevel.FinalExitLevel finalExitLevel = new FinalExitLevel.FinalExitLevel(stair, finalExit);

            double exitCapacity = finalExitLevel.CalcMergingFlowCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
