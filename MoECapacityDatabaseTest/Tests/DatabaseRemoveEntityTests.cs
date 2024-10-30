using MoECapacityCalc.DomainEntities;
using MoECapacityDatabaseTest.TestHelpers;
using MoECapacityCalc.Utilities.Associations;

namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]

    public class DatabaseRemoveEntityTests : TestDatabaseSetup
    {
        private Repositories _repositories = GetRepositories();

        [TestMethod]
        public void CanRemoveExitsFromDatabase()
        {
            SeedDatbase();

            using (var context = GetContext())
            {
                Assert.AreEqual(6, context.Exits.Count());

                var storeyExit1 = context.Exits.FirstOrDefault(x => x.Name == "storey exit 1");
                var finalExit1 = context.Exits.FirstOrDefault(x => x.Name == "final exit 1");
                var storeyExit2 = context.Exits.FirstOrDefault(x => x.Name == "storey exit 2");
                var finalExit2 = context.Exits.FirstOrDefault(x => x.Name == "final exit 2");
                var storeyExit3 = context.Exits.FirstOrDefault(x => x.Name == "storey exit 3");
                var finalExit3 = context.Exits.FirstOrDefault(x => x.Name == "final exit 3");

                _repositories.ExitsRepository.Remove(storeyExit1);
                _repositories.ExitsRepository.RemoveMany(new List<Exit> {
                    finalExit1,
                    storeyExit2,
                    finalExit2,
                    storeyExit3,
                    finalExit3
                });

                Assert.AreEqual(0, context.Exits.Count());
                Assert.AreEqual(false, context.Exits.Any(e => e.Name == storeyExit1.Name));
                Assert.AreEqual(false, context.Exits.Any(e => e.Name == finalExit3.Name));
                Assert.AreEqual(0, context.Associations.Count(e => e.ObjectType == finalExit3.GetType().ToString()));
                Assert.AreEqual(0, context.Associations.Count(e => e.SubjectType == finalExit3.GetType().ToString()));
            }

        }

        [TestMethod]
        public void CanRemoveStairsFromDatabase()
        {
            SeedDatbase();

            using (var context = GetContext())
            {
                Assert.AreEqual(2, context.Stairs.Count());

                var stair1 = context.Stairs.FirstOrDefault(x => x.Name == "stair 1");
                var stair2 = context.Stairs.FirstOrDefault(x => x.Name == "stair 2");

                _repositories.StairsRepository.Remove(stair1);
                _repositories.StairsRepository.RemoveMany(new List<Stair> {
                    stair2
                });

                Assert.AreEqual(0, context.Stairs.Count());
                Assert.AreEqual(false, context.Stairs.Any(e => e.Name == stair1.Name));
                Assert.AreEqual(false, context.Stairs.Any(e => e.Name == stair2.Name));
                Assert.AreEqual(0, context.Associations.Count(e => e.ObjectType == stair2.GetType().ToString()));
                Assert.AreEqual(0, context.Associations.Count(e => e.SubjectType == stair2.GetType().ToString()));

            }

        }

        [TestMethod]
        public void CanRemoveAreasFromDatabase()
        {
            SeedDatbase();

            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Areas.Count());

                var area1 = context.Areas.FirstOrDefault(x => x.Name == "area 1");

                _repositories.AreasRepository.RemoveMany(new List<Area> {
                    area1
                });

                Assert.AreEqual(0, context.Areas.Count());
                Assert.AreEqual(false, context.Areas.Any(e => e.Name == area1.Name));
                Assert.AreEqual(0, context.Associations.Count(e => e.ObjectType == area1.GetType().ToString()));
                Assert.AreEqual(0, context.Associations.Count(e => e.SubjectType == area1.GetType().ToString()));
            }

        }

        [TestMethod]
        public void CanRemoveAssociationsFromDatabase()
        {
            SeedDatbase();

            using (var context = GetContext())
            {
                Assert.AreEqual(9, context.Associations.Count());

                var association1 = context.Associations.FirstOrDefault(x => x.ObjectType == "Area");
                var association2 = context.Associations.FirstOrDefault(x => x.ObjectType == "Stair");
                var association3 = context.Associations.FirstOrDefault(x => x.ObjectType == "Exit");

                _repositories.AssociationsRepository.Remove(association1);
                _repositories.AssociationsRepository.RemoveMany(new List<Association> {
                    association2,
                    association3
                });

                Assert.AreEqual(6, context.Associations.Count());
                Assert.AreEqual(false, context.Associations.Any(a => a.Id == association1.Id));
                Assert.AreEqual(false, context.Associations.Any(a => a.Id == association2.Id));
                Assert.AreEqual(false, context.Associations.Any(a => a.Id == association3.Id));
            }

        }



    }
}
