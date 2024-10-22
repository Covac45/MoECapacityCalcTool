using MoECapacityCalc.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Areas;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.CalcServices;
using MoECapacityCalc.Utilities.Associations;


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

            var area1 = GetAreaTestData();

            double exitCapacity = new AreaCalcService(area1).CalcDiscountedExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
