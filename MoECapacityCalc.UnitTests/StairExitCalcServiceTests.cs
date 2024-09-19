using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests
{
    public class StairExitCalcServiceTests
    {

        public (List<Exit>, List<Exit>) InitialiseLists()
        {
            Exit exit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit2 = new Exit("storey exit 2", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit3 = new Exit("storey exit 3", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit4 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit5 = new Exit("final exit 2", ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit6 = new Exit("final exit 3", ExitType.finalExit, DoorSwing.with, 1050);

            List<Exit> storeyExits = new List<Exit> { exit1, exit2, exit3 };
            List<Exit> finalExits = new List<Exit> { exit4, exit5, exit6 };

            return (storeyExits, finalExits);
        }
        [TestCase(660)]
        public void TotalStoreyExitCapacityTest(double expectedExitCapacity)
        {
            var (storeyExits, finalExits) = InitialiseLists();

            StairExitCalcsService stairExitCalcsService = new StairExitCalcsService(storeyExits, finalExits);

            double exitCapacity = stairExitCalcsService.TotalStoreyExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }
    }
}
