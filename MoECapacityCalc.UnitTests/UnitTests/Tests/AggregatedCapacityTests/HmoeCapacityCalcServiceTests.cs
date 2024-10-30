using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.AggregatedCapacityTests
{
    public class HmoeCapacityCalcServiceTests : TestAreas
    {

        private IHorizontalEscapeCapacityCalcService CreateTarget()
        {
            var stairExitCalcService = new StairExitCalcService();
            var exitCapacityCalcService = new ExitCapacityCalcService();
            var exitCapacityStructCapService = new ExitCapacityStructCapService();
            return new HorizontalEscapeCapacityCalcService(exitCapacityCalcService, exitCapacityStructCapService, stairExitCalcService);
        }

        [SetUp]

        public void Setup()
        {
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit and  2x stairs each with 1x separate final exit.
        [TestCase(325)]
        public void AreaHMoECapacityTest1(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaTestData1();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit and 2x stairs which share 2x final exits
        [TestCase(325)]
        public void AreaHMoECapacityTest2(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaTestData2();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //Test for area on final exit level with 1x storey exit, 2x stairs each with 1x dedicated final exit and which share 1x final exit.
        [TestCase(315)]
        public void AreaHMoECapacityTest3(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaTestData3();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(850)]
        public void AreaHMoECapacityTest4(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaTestData4();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(240)]
        public void AreaHMoECapacityTest5(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaTestData5();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //Test for area on upper level with 1x storey exit and 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(880)]
        public void AreaHMoECapacityTest6(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaTestData6();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an empty area
        [TestCase(0)]
        public void EmptyAreaHMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetEmptyArea();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a stair and no exits
        [TestCase(0)]
        public void AreaContainingStairWithNoExitsHMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaWithStairWithNoExits();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a storey exit only
        [TestCase(60)]
        public void AreaContainingStoreyExitOnlyHMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaWithStoreyExitOnly();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a final exit only
        [TestCase(60)]
        public void AreaContainingFinalExitOnlyHMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaWithFinalExitOnly();

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalHMoECapacity(exitCapacityStructs, area1).HmoeCapacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }
    }
}
