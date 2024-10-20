using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Services;


namespace MoECapacityCalc.UnitTests.UnitTests.Tests
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

            List<Exit> stair1Exits = new List<Exit> { finalExit1, storeyExit1 };

            Association stair1Associations = new Association(stair1Exits);

            Stair stair1 = new Stair("stair 1", stairWidth, floorsServed, 0, stair1Associations);

            double stairCapacity = new StairCapacityCalcService(stair1).CalcStairCapacity();
            double stairCapacityPerFloor = new StairCapacityCalcService(stair1).CalcStairCapacityPerFloor();
            Assert.That(stairCapacity, Is.EqualTo(expectedStairCapacity));
            Assert.That(stairCapacityPerFloor, Is.EqualTo(expectedStairCapacityPerFloor));
        }

    }
}
