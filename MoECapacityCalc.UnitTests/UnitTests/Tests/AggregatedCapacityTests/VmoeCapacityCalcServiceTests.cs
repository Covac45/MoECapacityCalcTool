using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.VMoECalcServices;
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


        //ValidationTests
        //Test for an empty area
        [TestCase(0)]
        public void EmptyAreaVMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetEmptyArea();

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a stair and no exits
        [TestCase(0)]
        public void AreaContainingStairWithNoExitsVMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaWithStairWithNoExits();

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a storey exit only
        [TestCase(0)]
        public void AreaContainingStoreyExitOnlyVMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaWithStoreyExitOnly();

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }

        //Test for an area containing a final exit only
        [TestCase(0)]
        public void AreaContainingFinalExitOnlyVMoECapacityTest(double expectedExitCapacity)
        {
            var target = CreateTarget();
            var area1 = GetAreaWithFinalExitOnly();

            var stairCapacityStructs = target.CalcStairCapacities(area1);
            var vmoeCapacity = target.CalcTotalVMoECapacity(stairCapacityStructs, area1);

            Assert.That(vmoeCapacity, Is.EqualTo(expectedExitCapacity));
        }






    }
}
