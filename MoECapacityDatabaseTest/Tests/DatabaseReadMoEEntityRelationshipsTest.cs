using MoECapacityDatabaseTest.TestHelpers;


namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]
    public class DatabaseReadMoEEntityRelationshipsTest : TestDatabaseSetup
    {

        private Repositories _repositories = GetRepositories();

        [TestMethod]
        public void CanReadStairRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var stair = _repositories.StairsRepository.GetById(context.Stairs.First(e => e.Name == "stair 1").Id);
            var stairs = _repositories.StairsRepository.GetAll();


            Assert.AreNotEqual(stair.Relationships.ExitRelationships.Count, 0);
            Assert.AreNotEqual(stairs.Any(e => e.Relationships.ExitRelationships.Count != 0), false);

        }

        [TestMethod]
        public void CanReadExitRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var exit = _repositories.ExitsRepository.GetById(context.Exits.First(e => e.Name == "storey exit 3").Id);
            var exits = _repositories.ExitsRepository.GetAll();

            Assert.AreNotEqual(exit.Relationships.ExitRelationships.Count, 0);
            Assert.AreNotEqual(exits.Any(e => e.Relationships.ExitRelationships.Count != 0), false);
        }

        [TestMethod]
        public void CanReadAreaRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var area = _repositories.AreasRepository.GetById(context.Areas.First(e => e.Name == "area 1").Id);
            var areas = _repositories.AreasRepository.GetAll();

            Assert.AreNotEqual(area.Relationships.ExitRelationships.Count, 0);
            Assert.AreNotEqual(areas.Any(e => e.Relationships.ExitRelationships.Count != 0), false);
        }
    }
}
