using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.UnitTests.TestHelpers;
using MoECapacityCalc.Utilities.Services;
using NUnit.Framework.Internal;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests
{
    public class ExitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Exit capacity unit tests
        [TestCase(600, DoorSwing.with, 0)]
        [TestCase(750, DoorSwing.with, 60)]
        [TestCase(850, DoorSwing.with, 110)]
        [TestCase(1050, DoorSwing.with, 220)]
        [TestCase(1200, DoorSwing.with, 250)]
        [TestCase(1400, DoorSwing.against, 60)]
        public void ExitCapacityTest(double exitWidth, DoorSwing doorSwing, double expectedExitCapacity)
        {
            Exit exit1 = new Exit("exit 1", ExitType.exit, doorSwing, exitWidth);

            double exitCapacity = new ExitCapacityCalcService(exit1).CalcExitCapacity();
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