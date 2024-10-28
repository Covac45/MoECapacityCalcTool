using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.CalcServices;


namespace MoECapacityCalc.UnitTests.UnitTests.Tests
{
    public class AreaCapacityTests : TestArea
    {

        [SetUp]

        public void Setup()
        {
        }

        [TestCase(325)]
        public void AreaFinalExitLevelCapacityTest(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData1();

            double exitCapacity = new AreaCalcService(area1).CalcDiscountedExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
