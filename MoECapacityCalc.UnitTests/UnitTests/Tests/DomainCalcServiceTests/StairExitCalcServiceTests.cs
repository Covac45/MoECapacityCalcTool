using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.DomainCalcServices.StairExitCalcServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests.DomainCalcServiceTests
{
    public class StairExitCalcServiceTests
    {

        public Stair InitialiseLists()
        {
            Exit exit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit4 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit5 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit6 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            Stair stair1 = new Stair("stair 1", 1000, 5, 1, false);

            stair1.Relationships.ExitRelationships =
                [new Relationship<Stair,Exit>(stair1, exit1),
                new Relationship<Stair,Exit>(stair1, exit2),
                new Relationship<Stair,Exit>(stair1, exit3),
                new Relationship<Stair,Exit>(stair1, exit4),
                new Relationship<Stair,Exit>(stair1, exit5),
                new Relationship<Stair,Exit>(stair1, exit6)];

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
