using MoECapacityCalc.Database;
using MoECapacityCalc.Database.Data_Logic.Repositories;
using MoECapacityCalc.Stairs;
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

        [TestMethod]
        public void CanReadStairRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var stair = new StairsRepository(context, new AssociationsRepository(context)).GetStairById(context.Stairs.First(s => s.Name == "stair 1").Id);

            Assert.AreNotEqual(stair.Relationships.ExitRelationships.Count, 0);
        }
        
        [TestMethod]
        public void CanReadExitRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var exit = new ExitsRepository(context, new AssociationsRepository(context)).GetExitById(context.Exits.First(e => e.Name == "storey exit 3").Id);

            Assert.AreNotEqual(exit.Relationships.ExitRelationships.Count, 0);

        }
        
        [TestMethod]
        public void CanReadAreaRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var area = new AreasRepository(context, new AssociationsRepository(context)).GetAreaById(context.Areas.First(s => s.Name == "area 1").Id);

            Assert.AreNotEqual(area.Relationships.ExitRelationships.Count, 0);

        }
    }
}
