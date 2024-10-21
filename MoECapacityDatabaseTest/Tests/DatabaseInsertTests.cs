using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoECapacityCalc.Database;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;
using MoECapacityDatabaseTest.TestHelpers;
using System.Reflection.Emit;

namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]
    public class DatabaseInsertTests : TestDatabaseSetup
    {
        [TestMethod]
        public void CanInsertExitsIntoDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {

                Exit storeyExit1 = new Exit { ExitId = Guid.NewGuid(), ExitName = "storey exit 1", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit1 = new Exit { ExitId = Guid.NewGuid(), ExitName = "final exit 1", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit2 = new Exit { ExitId = Guid.NewGuid(), ExitName = "storey exit 2", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit2 = new Exit { ExitId = Guid.NewGuid(), ExitName = "final exit 2", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit3 = new Exit { ExitId = Guid.NewGuid(), ExitName = "storey exit 3", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit3 = new Exit { ExitId = Guid.NewGuid(), ExitName = "final exit 3", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                context.Exits.AddRange(
                    storeyExit1,
                    finalExit1,
                    storeyExit2,
                    finalExit2,
                    storeyExit3,
                    finalExit3);

                context.SaveChanges();

                Assert.AreNotEqual(0, context.Exits.Count());
                Assert.AreNotEqual("", context.Exits.First().ExitId.ToString());
                Assert.AreNotEqual(null, context.Exits.First().ExitId.ToString());
            }

        }

        [TestMethod]
        public void CanInsertStairsIntoDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {

                Stair stair1 = new Stair
                {
                    StairId = Guid.NewGuid(),
                    StairName = "stair 1",
                    StairWidth = 1000,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                Stair stair2 = new Stair
                {
                    StairId = Guid.NewGuid(),
                    StairName = "stair 2",
                    StairWidth = 1100,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                context.Stairs.AddRange(stair1, stair2);

                context.SaveChanges();

                Assert.AreNotEqual(0, context.Stairs.Count());
                Assert.AreNotEqual("", context.Stairs.First().StairId.ToString());
                Assert.AreNotEqual(null, context.Stairs.First().StairId.ToString());
            }
        }

        [TestMethod]
        public void CanInsertRelationshipsIntoDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {
                Exit storeyExit1 = new Exit { ExitId = Guid.NewGuid(), ExitName = "storey exit 1", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit1 = new Exit { ExitId = Guid.NewGuid(), ExitName = "final exit 1", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit2 = new Exit { ExitId = Guid.NewGuid(), ExitName = "storey exit 2", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit2 = new Exit { ExitId = Guid.NewGuid(), ExitName = "final exit 2", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Stair stair1 = new Stair
                {
                    StairId = Guid.NewGuid(),
                    StairName = "stair 1",
                    StairWidth = 1000,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                Stair stair2 = new Stair
                {
                    StairId = Guid.NewGuid(),
                    StairName = "stair 2",
                    StairWidth = 1100,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };



                context.Relationships.AddRange(
                    new Relationship(stair1, storeyExit1),
                    new Relationship(stair1, finalExit1),
                    new Relationship(stair2, storeyExit2),
                    new Relationship(stair2, finalExit2));

                context.SaveChanges();

                Assert.AreNotEqual(0, context.Relationships.Count());
                Assert.AreNotEqual("", context.Relationships.First().RelationshipId.ToString());
                Assert.AreNotEqual(null, context.Relationships.First().RelationshipId.ToString());
            }

        }

    }
}