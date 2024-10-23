using MoECapacityCalc.Database;
using MoECapacityCalc.Database.Data_Logic.Repositories;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Exits;
using MoECapacityDatabaseTest.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]
    public class ReadRelationshipsTest : TestDatabaseSetup
    {

        private Repositories _repositories = GetRepositories();

        [TestMethod]
        public void CanReadStairRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var stair = _repositories.StairsRepository.GetById(context.Stairs.First(e => e.Name == "stair 1").Id);

            Assert.AreNotEqual(stair.Relationships.ExitRelationships.Count, 0);
        }
        
        [TestMethod]
        public void CanReadExitRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var exit = _repositories.ExitsRepository.GetById(context.Exits.First(e => e.Name == "storey exit 3").Id);

            Assert.AreNotEqual(exit.Relationships.ExitRelationships.Count, 0);

        }
        
        [TestMethod]
        public void CanReadAreaRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var area = _repositories.AreasRepository.GetById(context.Areas.First(e => e.Name == "area 1").Id);

            Assert.AreNotEqual(area.Relationships.ExitRelationships.Count, 0);

        }
    }
}
