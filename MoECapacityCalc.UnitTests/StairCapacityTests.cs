using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Datastructs;


namespace MoECapacityCalc.UnitTests
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
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.finalExit, DoorSwing.with, 1400);

            List<Exit> finalExits = new List<Exit> { finalExit1 };
            List<Exit> storeyExits = new List<Exit> { storeyExit1 };

            Stair stair1 = new Stair("stair 1", stairWidth, floorsServed, 0, finalExits, storeyExits);

            double stairCapacity = stair1.CalcStairCapacity();
            double stairCapacityPerFloor = stair1.CalcStairCapacityPerFloor();
            Assert.That(stairCapacity, Is.EqualTo(expectedStairCapacity));
            Assert.That(stairCapacityPerFloor, Is.EqualTo(expectedStairCapacityPerFloor));
        }

    }
}
