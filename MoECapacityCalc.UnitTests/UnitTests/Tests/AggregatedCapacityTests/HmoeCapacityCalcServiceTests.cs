using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.DiscountingService;
using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.ApplicationLayer.Utilities.DomainCalcServices.StairCalcServices.EvacuationStrategies;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.AggregatedCapacityTests
{
    public class HmoeCapacityCalcServiceTests : TestAreas
    {

        private IHorizontalEscapeCapacityCalcService CreateTarget(Area area)
        {
            var stairExitCalcService = new StairExitCalcService();
            var exitCapacityCalcService = new ExitCapacityCalcService();
            var exitCapacityStructCapService = new ExitCapacityStructCapService();

            var stairFinalExitWidthStrategy = new AreaStairFinalExitWidthStrategy(area);
            var stairFinalExitCapacityStrategy = new AreaStairFinalExitCapacityStrategy(area);
            var evacuationStrategy = new SimultaneousEvacuationStrategy();
            var stairCapacityCalcService = new StairCapacityCalcService(stairFinalExitWidthStrategy, stairFinalExitCapacityStrategy, evacuationStrategy);
            
            var exitCapacityStructsService = new ExitCapacityStructsService(exitCapacityCalcService, stairCapacityCalcService, stairExitCalcService);
            var cachedExitCapacityStructsService = new CachedExitCapacityStructService(exitCapacityStructsService);

            var discountingService = new DiscountingAndCappingService();


            return new HorizontalEscapeCapacityCalcService(exitCapacityCalcService, exitCapacityStructCapService, stairExitCalcService, stairCapacityCalcService, cachedExitCapacityStructsService, discountingService);
        }

        [SetUp]

        public void Setup()
        {
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit and  2x stairs each with 1x separate final exit.
        [TestCase(325)]
        public void AreaHMoECapacityTest1(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData1();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit and 2x stairs which share 2x final exits
        [TestCase(325)]
        public void AreaHMoECapacityTest2(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData2();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //Test for area on final exit level with 1x storey exit, 2x stairs each with 1x dedicated final exit and which share 1x final exit.
        [TestCase(315)]
        public void AreaHMoECapacityTest3(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData3();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(850)]
        public void AreaHMoECapacityTest4(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData4();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(240)]
        public void AreaHMoECapacityTest5(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData5();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }


        //Test for area on upper level with 1x storey exit and 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(880)]
        public void AreaHMoECapacityTest6(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData6();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an empty area
        [TestCase(0)]
        public void EmptyAreaHMoECapacityTest(double expectedExitCapacity)
        {
            var area1 = GetEmptyArea();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a stair and no exits
        [TestCase(0)]
        public void AreaContainingStairWithNoExitsHMoECapacityTest(double expectedExitCapacity)
        {
            var area1 = GetAreaWithStairWithNoExits();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a storey exit only
        [TestCase(60)]
        public void AreaContainingStoreyExitOnlyHMoECapacityTest(double expectedExitCapacity)
        {
            var area1 = GetAreaWithStoreyExitOnly();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a final exit only
        [TestCase(60)]
        public void AreaContainingFinalExitOnlyHMoECapacityTest(double expectedExitCapacity)
        {
            var area1 = GetAreaWithFinalExitOnly();
            var target = CreateTarget(area1);

            List<ExitCapacityStruct> exitCapacityStructs = target.CalcExitCapacities(area1);
            var exitCapacity = target.CalcTotalDiscountedHMoECapacity(exitCapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }
    }
}
