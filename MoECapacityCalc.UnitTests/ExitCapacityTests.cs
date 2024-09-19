using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs.StairFinalExits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Datastructs;
using NUnit.Framework.Internal;
using MoECapacityCalc.UnitTests.TestHelpers;

namespace MoECapacityCalc.UnitTests
{
    public class ExitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Merging flow capacity tests
        [TestCase(600, DoorSwing.with, 0)]
        [TestCase(750, DoorSwing.with, 60)]
        [TestCase(850, DoorSwing.with, 110)]
        [TestCase(1050, DoorSwing.with, 220)]
        [TestCase(1200, DoorSwing.with, 250)]
        [TestCase(1400, DoorSwing.against, 60)]
        public void ExitCapacityTest(double exitWidth, DoorSwing doorSwing, double expectedExitCapacity)
        {
            Exit exit1 = new Exit("exit 1", ExitType.exit, doorSwing, exitWidth);

            double exitCapacity = exit1.CalcExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        [Test]
        public void Test()
        {
            var exit1 = ExitTestHelper.GetDefaultStoreyExitBuilder()
                .Build();
            var exit2 = ExitTestHelper.GetDefaultFinalExitBuilder()
                .Build();
        }
    }
}