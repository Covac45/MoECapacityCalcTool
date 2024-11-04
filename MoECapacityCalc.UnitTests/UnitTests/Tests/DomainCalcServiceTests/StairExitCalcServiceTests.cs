using MoECapacityCalc.Domain.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.DomainCalcServiceTests
{
    public class StairExitCalcServiceTests
    {

        public Stair InitialiseLists()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit storeyExit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit storeyExit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);
            Exit finalExit2 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);
            Exit finalExit3 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            Stair stair1 = new Stair("stair 1", 1000, 5, 1, false);

            stair1.Relationships.ExitRelationships =
                [new Relationship<Stair,Exit>(stair1,  RelativeDirection.from, storeyExit1),
                new Relationship<Stair,Exit>(stair1,  RelativeDirection.from, storeyExit2),
                new Relationship<Stair,Exit>(stair1,  RelativeDirection.from, storeyExit3),
                new Relationship<Stair,Exit>(stair1,  RelativeDirection.to, finalExit1),
                new Relationship<Stair,Exit>(stair1,  RelativeDirection.to, finalExit2),
                new Relationship<Stair,Exit>(stair1,  RelativeDirection.to, finalExit3)];

            return stair1;
        }

        //Storey exit capacity unit test
        [TestCase(660)]
        public void TotalStoreyExitCapacityTest(double expectedExitCapacity)
        {
            Stair stair1 = InitialiseLists();

            StairExitCalcService stairExitCalcsService = new StairExitCalcService();

            double exitCapacity = stairExitCalcsService.TotalStoreyExitCapacity(stair1);
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }
    }
}
