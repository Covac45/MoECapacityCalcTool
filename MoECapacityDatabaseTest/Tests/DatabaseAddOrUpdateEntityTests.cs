using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoECapacityCalc.Database;
using MoECapacityCalc.Database.Data_Logic.Repositories;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityDatabaseTest.TestHelpers;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]

    public class DatabaseAddOrUpdateEntityTests : TestDatabaseSetup
    {
        private Repositories _repositories = GetRepositories();

        [TestMethod]
        public void CanAddOrUpdateExitsToDatabase()
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

                _repositories.ExitsRepository.AddOrUpdate(storeyExit4);
                _repositories.ExitsRepository.AddOrUpdateMany(new List<Exit> {
                    finalExit4,
                    storeyExit5,
                    finalExit5,
                    storeyExit6,
                    finalExit6
                });

                //check creation
                Assert.AreEqual(6, context.Exits.Count());
                Assert.AreEqual(true, context.Exits.Any(e => e.Name == storeyExit4.Name));
                Assert.AreEqual(true, context.Exits.Any(e => e.Name == finalExit6.Name));

                //check update
                var oldStoreyExit5Id = storeyExit5.Id;

                storeyExit5.ExitWidth = 1200;
                _repositories.ExitsRepository.AddOrUpdate(storeyExit5);

                Assert.AreEqual(true, context.Exits.Where(e => e.Id == oldStoreyExit5Id).Any(e => e.ExitWidth == storeyExit5.ExitWidth));
            }

        }

        [TestMethod]
        public void CanAddOrUpdateStairsIntoDatabase()
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

                _repositories.StairsRepository.AddOrUpdate(stair3);
                _repositories.StairsRepository.AddOrUpdateMany(new List<Stair> {
                    stair4
                });


                //check creation
                Assert.AreEqual(2, context.Stairs.Count());
                Assert.AreEqual(true, context.Stairs.Any(e => e.Name == stair3.Name));
                Assert.AreEqual(true, context.Stairs.Any(e => e.Name == stair4.Name));

                //check update
                var oldStair4Id = stair4.Id;

                stair3.StairWidth = 1200;
                _repositories.StairsRepository.AddOrUpdate(stair4);

                Assert.AreEqual(true, context.Stairs.Where(e => e.Id == oldStair4Id).Any(e => e.StairWidth == stair4.StairWidth));
            }
        }

        public void CanAddOrUpdateAreasToDatabase()
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

                _repositories.AreasRepository.AddOrUpdate(area2);
                _repositories.AreasRepository.AddOrUpdateMany(new List<Area> {
                    area3
                });

                //check creation
                Assert.AreEqual(2, context.Areas.Count());
                Assert.AreEqual(true, context.Areas.Any(e => e.Name == area2.Name));
                Assert.AreEqual(true, context.Areas.Any(e => e.Name == area3.Name));

                // check update
                var oldArea3Id = area3.Id;

                area3.FloorLevel = 1;
                _repositories.AreasRepository.AddOrUpdate(area3);

                Assert.AreEqual(true, context.Areas.Where(e => e.Id == oldArea3Id).Any(e => e.FloorLevel == area3.FloorLevel));
            }
        }

        [TestMethod]
        public void CanAddOrUpdateAssociationsToDatabase()
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

                var association1 = new Association(stair1, storeyExit1);
                var association2 = new Association(stair1, finalExit1);
                var association3 = new Association(stair2, storeyExit2);
                var association4 = new Association(stair2, finalExit2);

                _repositories.AssociationsRepository.AddOrUpdate(association1);
                _repositories.AssociationsRepository.AddOrUpdateMany(new List<Association>() {
                    association2,
                    association3,
                    association4
                });

                Assert.AreNotEqual(0, context.Associations.Count());
                Assert.AreNotEqual("", context.Associations.First().Id.ToString());
                Assert.AreNotEqual(null, context.Associations.First().Id.ToString());

                // check update
                var oldAssociation4Id = association4.Id;

                association4.SubjectId = finalExit1.Id;
                _repositories.AssociationsRepository.AddOrUpdate(association4);

                Assert.AreEqual(true, context.Associations.Where(e => e.Id == oldAssociation4Id).Any(e => e.SubjectId == finalExit1.Id));
            }

        }

    }
}