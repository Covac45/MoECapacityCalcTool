using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoECapacityCalc.Database;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityDatabaseTest.TestHelpers;
using System.Reflection.Emit;

namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]

    public class DatabaseCreateEntityTests : TestDatabaseSetup
    {
        private Repositories _repositories = GetRepositories();

        [TestMethod]
        public void CanAddExitsToDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {

                Exit storeyExit4 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 4", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit4 = new Exit { Id = Guid.NewGuid(), Name = "final exit 4", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit5 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 5", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit5 = new Exit { Id = Guid.NewGuid(), Name = "final exit 5", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                Exit storeyExit6 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 6", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
                Exit finalExit6 = new Exit { Id = Guid.NewGuid(), Name = "final exit 6", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

                _repositories.ExitsRepository.Add(storeyExit4);
                _repositories.ExitsRepository.AddMany(new List<Exit> {
                    finalExit4,
                    storeyExit5,
                    finalExit5,
                    storeyExit6,
                    finalExit6
                });

                Assert.AreEqual(6, context.Exits.Count());
                Assert.AreEqual(true, context.Exits.Any(e => e.Name == storeyExit4.Name));
                Assert.AreEqual(true, context.Exits.Any(e => e.Name == finalExit6.Name));
            }

        }

        [TestMethod]
        public void CanAddStairsIntoDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {

                Stair stair3 = new Stair
                {
                    Id = Guid.NewGuid(),
                    Name = "stair 3",
                    StairWidth = 1000,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                Stair stair4 = new Stair
                {
                    Id = Guid.NewGuid(),
                    Name = "stair 4",
                    StairWidth = 1100,
                    FloorsServed = 3,
                    FinalExitLevel = 0
                };

                _repositories.StairsRepository.Add(stair3);
                _repositories.StairsRepository.AddMany(new List<Stair> {
                    stair4
                });

                Assert.AreEqual(2, context.Stairs.Count());
                Assert.AreEqual(true, context.Stairs.Any(e => e.Name == stair3.Name));
                Assert.AreEqual(true, context.Stairs.Any(e => e.Name == stair4.Name));
            }
        }

        public void CanAddAreasToDatabase()
        {
            ResetDatbase();

            using (var context = GetContext())
            {
                Area area2 = new Area
                {
                    Id = Guid.NewGuid(),
                    Name = "area 2",
                    FloorLevel = 0
                };

                Area area3 = new Area
                {
                    Id = Guid.NewGuid(),
                    Name = "area 3",
                    FloorLevel = 0
                };

                _repositories.AreasRepository.Add(area2);
                _repositories.AreasRepository.AddMany(new List<Area> {
                    area3
                });

                Assert.AreEqual(2, context.Areas.Count());
                Assert.AreEqual(true, context.Areas.Any(e => e.Name == area2.Name));
                Assert.AreEqual(true, context.Areas.Any(e => e.Name == area3.Name));
            }
        }

        [TestMethod]
        public void CanAddAssociationsToDatabase()
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

                _repositories.AssociationsRepository.Add(new Association(stair1, storeyExit1));
                _repositories.AssociationsRepository.AddMany(new List<Association>() {
                    new Association(stair1, finalExit1),
                    new Association(stair2, storeyExit2),
                    new Association(stair2, finalExit2)
                });

                Assert.AreNotEqual(0, context.Associations.Count());
                Assert.AreNotEqual("", context.Associations.First().Id.ToString());
                Assert.AreNotEqual(null, context.Associations.First().Id.ToString());
            }

        }

    }
}