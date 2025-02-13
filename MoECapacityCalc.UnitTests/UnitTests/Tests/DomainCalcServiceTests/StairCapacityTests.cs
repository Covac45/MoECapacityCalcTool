using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.DomainCalcServices.StairCalcServices.ServiceFactory;
using MoECapacityCalc.Domain.DomainEntities.Datastructs;


namespace MoECapacityCalc.UnitTests.UnitTests.Tests.DomainCalcServiceTests
{
    public class StairTests
    {
        [SetUp]
        public void Setup()
        {
        }

        
        //Stair capacity tests
        [TestCase(800, 1, 50, 50)]
        [TestCase(900, 5, 50, 10)]
        [TestCase(1000, 1, 150, 150)]
        [TestCase(1050, 5, 310, 62)]
        [TestCase(1200, 5, 420, 84)]
        [TestCase(1400, 10, 775, 77.5)]
        public void StairCapacityTests(double stairWidth, int floorsServed, double expectedStairCapacity, double expectedStairCapacityPerFloor)
        {
            

            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1400);
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1400);

            Stair stair1 = new Stair("stair 1", stairWidth, floorsServed, 0, false);

            stair1.Relationships.ExitRelationships =
                [new Relationship<Stair, Exit>(stair1,  RelativeDirection.from, storeyExit1),
                new Relationship<Stair, Exit>(stair1,  RelativeDirection.to, finalExit1)];

            Area area1 = new Area(0, "Area 1", false);
            area1.Relationships.StairRelationships = [new Relationship<Area, Stair>( area1, RelativeDirection.to, stair1 )];

            var stairCapacity = new StairCalcServiceFactory().Create().CalcStairCapacity(stair1); ;
            double stairCapacityPerFloor = new StairCalcServiceFactory().Create().GetStairCapacityStruct(stair1).CapacityPerFloor;
            Assert.That(stairCapacity, Is.EqualTo(expectedStairCapacity));
            Assert.That(stairCapacityPerFloor, Is.EqualTo(expectedStairCapacityPerFloor));
        }

    }
}
