using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MoECapacityCalc.UnitTests
{
    public class StairTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Stair capacity tests
        [TestCase(800, 1, 50)]
        [TestCase(900, 5, 50)]
        [TestCase(1000, 1, 150)]
        [TestCase(1050, 5, 310)]
        [TestCase(1200, 5, 420)]
        [TestCase(1400, 10, 775)]
        public void StairCapacityTests(double stairWidth, int upperFloorsServed, double expectedStairCapacity)
        {
            Stair stair1 = new Stair(stairWidth, upperFloorsServed);

            double stairCapacity = stair1.CalcStairCapacity();
            Assert.That(stairCapacity, Is.EqualTo(expectedStairCapacity));
        }

    }
}
