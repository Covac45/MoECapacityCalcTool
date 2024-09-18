using MoECapacityCalc.Exits;
using MoECapacityCalc.Exits.Datastructs;

namespace MoECapacityCalc.UnitTests
{
    public class ExitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Exit Capacity tests
        [TestCase(750, ExitType.storeyExit, DoorSwing.with, 60)]
        [TestCase(850, ExitType.storeyExit, DoorSwing.with, 110)]
        [TestCase(1050, ExitType.storeyExit, DoorSwing.with, 220)]
        [TestCase(1200, ExitType.storeyExit, DoorSwing.with, 250)]
        [TestCase(1200, ExitType.storeyExit, DoorSwing.against, 60)]
        public void ExitCapacityTest(double exitWidth, ExitType exitType, DoorSwing doorSwing, double expectedExitCapacity)
        {
            Exit exit1 = new Exit(exitType, doorSwing, exitWidth);
            
            double exitCapacity = exit1.CalcExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Merging flow capacity tests
        [TestCase(1500, 1500, 75)]
        [TestCase(1000, 1200, 20)]
        public void MergingFlowCapacityTest(double exitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit exit1 = new Exit(ExitType.finalExit, DoorSwing.with, exitWidth);

            double exitCapacity = exit1.CalcMergingFlowCapacity(exitWidth, stairWidth);
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}