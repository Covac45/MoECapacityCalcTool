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

        //Test for area on final exit level with 1x storey exit, 1x final exit and  2x stairs each with 1x separate final exit.
        [TestCase(325)]
        public void AreaHMoECapacityTest1(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData1();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit and 2x stairs which share 2x final exits
        [TestCase(325)]
        public void AreaHMoECapacityTest2(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData2();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //Test for area on final exit level with 1x storey exit, 2x stairs each with 1x dedicated final exit and which share 1x final exit.
        [TestCase(315)]
        public void AreaHMoECapacityTest3(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData3();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(850)]
        public void AreaHMoECapacityTest4(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData4();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(240)]
        public void AreaHMoECapacityTest5(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData5();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //Test for area on upper level with 1x storey exit and 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(880)]
        public void AreaHMoECapacityTest6(double expectedExitCapacity)
        {

            var area1 = GetAreaTestData6();

            List<ExitCapacityStruct> exitCapacityStructs = new HorizontalEscapeCapacityCalcService().CalcExitCapacities(area1);
            var exitCapacity = new HorizontalEscapeCapacityCalcService().CalcTotalHMoECapacity(exitCapacityStructs);

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
