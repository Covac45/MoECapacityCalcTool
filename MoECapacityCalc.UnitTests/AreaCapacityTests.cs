using MoECapacityCalc.Exits.Datastructs;
using MoECapacityCalc.Exits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Areas;


namespace MoECapacityCalc.UnitTests
{
    public class AreaCapacityTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        //Merging flow capacity tests
        [TestCase(280)]
        public void AreaExitCapacityTest(double expectedExitCapacity)
        {
            Exit exit1 = new Exit(ExitType.storeyExit, DoorSwing.with, 1050);
            Exit exit2 = new Exit(ExitType.finalExit, DoorSwing.with, 1050);
            Exit exit3 = new Exit(ExitType.finalExit, DoorSwing.against, 1050);

            List<Exit> exits = new List<Exit>();
            exits.Add(exit1);
            exits.Add(exit2);
            exits.Add(exit3);

            Stair stair1 = new Stair(1000, 3, exit1);
            Stair stair2 = new Stair(1000, 3, exit2);

            List<Stair> stairs = new List<Stair>();
            stairs.Add(stair1);
            stairs.Add(stair2);

            Area area = new Area(exits, stairs);

            double exitCapacity = area.CalcDiscountedExitCapacity();
            Assert.That(exitCapacity, Is.EqualTo(expectedExitCapacity));
        }

    }
}
