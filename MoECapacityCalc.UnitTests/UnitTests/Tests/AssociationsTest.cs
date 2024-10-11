using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.UnitTests.UnitTests.TestData;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.UnitTests.UnitTests.Tests
{
    public class AssociationsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        public (Exit, Stair) InitialiseObjects()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            List<Exit> stair1Exits = new List<Exit> { storeyExit1, finalExit1 };

            List<Exit> stair1StoreyExit = [storeyExit1];

            Stair stair1 = new Stair("stair 1", 1000, 3, 0, new Associations(stair1Exits));
            List<Stair> stairs = new List<Stair> { stair1 };

            finalExit1.Associations = new Associations([storeyExit1], stairs);

            return (finalExit1, stair1);
        }

        //Association matrix unit tests
        [TestCase("storey exit 1", "final exit 1")]
        public void ExitCapacityTest(string expectedFE1StoreyExitAssociationNames, string expectedStair1FinalExitAssociationNames)
        {
            List<string> storeyExitNames = [expectedFE1StoreyExitAssociationNames];
            List<string> stair1FinalExitNames = [expectedStair1FinalExitAssociationNames];

            var (exit1, stair1) = InitialiseObjects();

            Assert.That(
                exit1.Associations.StoreyExits.Select(storeyExit => storeyExit.ExitName),
                Is.EqualTo(storeyExitNames.Select(storeyExitName => storeyExitName)));

            Assert.That(
                stair1.FinalExits.Select(finalExit => finalExit.ExitName),
                Is.EqualTo(stair1FinalExitNames.Select(finalExitName => finalExitName)));
        }

    }
}
