using MoECapacityCalc.Domain.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.DomainCalcServiceTests
{
    public class StairFinalExitTests
    {
        [SetUp]
        public void Setup()
        {
        }


        //merging flow capacity unit tests
        [TestCase(900, 1200, 0)]
        [TestCase(1200, 1200, 60)]
        [TestCase(1200, 1500, 15)]
        [TestCase(1050, 1050, 52.5)]
        public void MergingFlowCapacityTest(double exitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, exitWidth);
            List<Exit> stair1FinalExit = new List<Exit>() { finalExit1 };

            Stair stair1 = new Stair("stair 1", stairWidth, 1, 0, false);
            stair1.Relationships.ExitRelationships = [new Relationship<Stair, Exit>(stair1, RelativeDirection.to, finalExit1)]; ;

            double exitCapacity = new StairExitCalcService().CalcMergingFlowCapacities(new List<Stair>() { stair1 }).Single(s => s.Key == stair1).Value;
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

        /*
        //Stair capacity limiting factor unit tests
        [TestCase(1200, 850, 1200, 60)]
        [TestCase(1500, 850, 1200, 110)]
        [TestCase(1500, 1050, 1200, 120)]
        [TestCase(1500, 1050, 1500, 75)]
        public void StairFinalExitLevelCapacityTests(double finalExitWidth, double storeyExitWidth, double stairWidth, double expectedExitCapacity)
        {
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, finalExitWidth);
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, storeyExitWidth);

            Stair stair1 = new Stair("stair 1", stairWidth, 1, 0, false);

            stair1.Relationships.ExitRelationships =
                                [new Relationship<Stair, Exit>(stair1, storeyExit1),
                                new Relationship<Stair, Exit>(stair1, finalExit1)]; ;

            double exitCapacity = new StairExitCalcService().CalcFinalExitLevelCapacity(stair1);
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }*/

    }
}
