using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.VMoECalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.ServiceFactory;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.AggregatedCapacityTests
{
    public class VmoeCapacityCalcServiceTests : TestAreas
    {

        private IVerticalEscapeCapacityCalcService CreateTarget(Area area = null)
        {
            var stairCapacityCalcService = new StairCalcServiceFactory().Create(area);
            return new VerticalEscapeCapacityCalcService(stairCapacityCalcService);
        }


        [TestCase(230)]
        public void AreaVMoECapacityTest1(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData1();
            var target = CreateTarget(area1);

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }

        [TestCase(90)]
        public void AreaVMoECapacityTest2(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData7();
            var target = CreateTarget(area1);

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }

        [TestCase(110)]
        public void AreaVMoECapacityTest3(double expectedExitCapacity)
        {
            var area1 = GetAreaTestData8();
            var target = CreateTarget(area1);

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
