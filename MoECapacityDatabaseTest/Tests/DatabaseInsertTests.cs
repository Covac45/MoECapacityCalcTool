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

                Exit storeyExit1 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 1", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit1 = new Exit { Id = Guid.NewGuid(), Name = "final exit 1", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit2 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 2", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit2 = new Exit { Id = Guid.NewGuid(), Name = "final exit 2", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit3 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 3", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit3 = new Exit { Id = Guid.NewGuid(), Name = "final exit 3", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                context.Exits.AddRange(
                    storeyExit1,
                    finalExit1,
                    storeyExit2,
                    finalExit2,
                    storeyExit3,
                    finalExit3);

                context.SaveChanges();

                Assert.AreNotEqual(0, context.Exits.Count());
                Assert.AreNotEqual("", context.Exits.First().Id.ToString());
                Assert.AreNotEqual(null, context.Exits.First().Id.ToString());
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
                    Id = Guid.NewGuid(),
                    Name = "stair 1",
                    StairWidth = 1000,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                Stair stair2 = new Stair
                {
                    Id = Guid.NewGuid(),
                    Name = "stair 2",
                    StairWidth = 1100,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                context.Stairs.AddRange(stair1, stair2);

                context.SaveChanges();

                Assert.AreNotEqual(0, context.Stairs.Count());
                Assert.AreNotEqual("", context.Stairs.First().Id.ToString());
                Assert.AreNotEqual(null, context.Stairs.First().Id.ToString());
            }
        }

        [TestMethod]
        public void CanInsertRelationshipsIntoDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {
                Exit storeyExit1 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 1", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit1 = new Exit { Id = Guid.NewGuid(), Name = "final exit 1", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit2 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 2", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit2 = new Exit { Id = Guid.NewGuid(), Name = "final exit 2", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Stair stair1 = new Stair
                {
                    Id = Guid.NewGuid(),
                    Name = "stair 1",
                    StairWidth = 1000,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                Stair stair2 = new Stair
                {
                    Id = Guid.NewGuid(),
                    Name = "stair 2",
                    StairWidth = 1100,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                context.Associations.AddRange(
                    new Association(stair1, storeyExit1),
                    new Association(stair1, finalExit1),
                    new Association(stair2, storeyExit2),
                    new Association(stair2, finalExit2));

                context.SaveChanges();

                Assert.AreNotEqual(0, context.Associations.Count());
                Assert.AreNotEqual("", context.Associations.First().AssociationId.ToString());
                Assert.AreNotEqual(null, context.Associations.First().AssociationId.ToString());
            }

        }

    }
}