using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests
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

            List<Exit> exits = new List<Exit> { exit1, exit2, exit3, exit4, exit5, exit6 };


            List<Exit> storeyExits = new List<Exit> { exit1, exit2, exit3 };
            List<Exit> finalExits = new List<Exit> { exit4, exit5, exit6 };

            Associations stair1Associations = new Associations(exits);

            Stair stair1 = new Stair("stair 1", 1000, 5, 1, stair1Associations);

            return stair1;
        }

        //Storey exit capacity unit test
        [TestCase(660)]
        public void TotalStoreyExitCapacityTest(double expectedExitCapacity)
        {
            Stair stair1 = InitialiseLists();

            StairExitCalcService stairExitCalcsService = new StairExitCalcService(stair1);

            double exitCapacity = stairExitCalcsService.TotalStoreyExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }
    }
}
