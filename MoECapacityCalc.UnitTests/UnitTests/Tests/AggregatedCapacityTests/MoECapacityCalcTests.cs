using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.ApplicationLayer.Utilities.DomainCalcServices.StairCalcServices.EvacuationStrategies;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.HMoECalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.ExitCapacityCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.Strategies;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.MoECapacityCalcServices;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.VMoECalcServices;
using MoECapacityCalc.ApplicationLayer.Utilities.AggregatedCapacityCalcServices.DiscountingService;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.AggregatedCapacityTests
{
    public class MoECapacityCalcTests : TestAreas
    {
        private IMoeCapacityCalcService CreateTarget(Area area)
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

            var verticalEscapeCapacityService = new VerticalEscapeCapacityCalcService(stairCapacityCalcService);
            var discountingAndCappingService = new DiscountingAndCappingService();


            return new MoECapacityCalcService(cachedExitCapacityStructsService, verticalEscapeCapacityService, discountingAndCappingService);
        }

        //Test for area on final exit level with 1x storey exit, 1x final exit 4x stairs each with 1x dedicated final exit and which share 1x final exits between two stairs.
        [TestCase(850)]
        public void AreaHMoECapacityTest4(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData4();
            var target = CreateTarget(area1);

            var MoECapacityStructs = target.GetMoECapacityStructs(area1);
            var exitCapacity = target.GetTotalDiscountedMoECapacity(MoECapacityStructs, area1).Capacity;

            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
