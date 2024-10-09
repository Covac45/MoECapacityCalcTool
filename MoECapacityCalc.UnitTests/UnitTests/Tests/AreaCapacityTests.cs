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

            var (exits, stairs) = GetAreaTestData();

            Area area1 = new Area(0, exits, stairs);

            double exitCapacity = area1.CalcDiscountedExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
