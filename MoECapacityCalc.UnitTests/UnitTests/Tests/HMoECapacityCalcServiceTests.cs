using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.CalcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests
{
    public class HMoECapacityCalcServiceTests : TestArea
    {
        
        [SetUp]

        public void Setup()
        {
        }

        //Test for two stairs each with separate final exits.
        [TestCase(325)]
        public void AreaHMoECapacityTest1(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData1();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //test for two stairs which share a final exit
        [TestCase(325)]
        public void AreaHMoECapacityTest2(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData2();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //test for two stairs each with their own final exit and which share a final exit.

    }
}
