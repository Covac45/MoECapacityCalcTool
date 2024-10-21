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
    public class RelationshipsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        public (Exit, Stair) InitialiseObjects()
        {
            Exit storeyExit1 = new Exit("storey exit 1", ExitType.storeyExit, DoorSwing.with, 1050);
            Exit finalExit1 = new Exit("final exit 1", ExitType.finalExit, DoorSwing.with, 1050);

            Stair stair1 = new Stair("stair 1", 1000, 3, 0);

            stair1.Relationships =
                [new Relationship(stair1, storeyExit1),
                new Relationship(stair1, finalExit1)];

            finalExit1.Relationships = [new Relationship(finalExit1, storeyExit1)];

            return (finalExit1, stair1);
        }

        //Association matrix unit tests
        [TestCase("Exit", "Exit")]
        public void ExitCapacityTest(string expectedFE1StoreyExitRelationshipType, string expectedStair1FinalExitAssociationNames)
        {
            List<string> storeyExitTypes = [expectedFE1StoreyExitRelationshipType];
            List<string> stair1FinalExitType = [expectedFE1StoreyExitRelationshipType, expectedStair1FinalExitAssociationNames];

            var (finalExit1, stair1) = InitialiseObjects();

            Assert.That(
                finalExit1.Relationships.Select(storeyExit => storeyExit.SubjectType),
                Is.EqualTo(storeyExitTypes.Select(storeyExitType => storeyExitType)));

            Assert.That(
                stair1.Relationships.Select(finalExit => finalExit.SubjectType),
                Is.EqualTo(stair1FinalExitType.Select(finalExitType => finalExitType)));
        }

    }
}
