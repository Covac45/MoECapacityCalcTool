using MoECapacityCalc.Database;
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

            var stair = new MoEObjectRepository(context).GetStairById(context.Stairs.First().StairId);

            Assert.AreNotEqual(stair.Relationships.ExitRelationships.Count, 0);

        }
        
        [TestMethod]
        public void CanReadExitRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var exit = new MoEObjectRepository(context).GetExitById(context.Exits.First(e => e.ExitName == "storey exit 3").ExitId);

            Assert.AreNotEqual(exit.Relationships.ExitRelationships.Count, 0);

        }
        
        [TestMethod]
        public void CanReadAreaRealtionships()
        {
            using var context = GetContext();
            SeedDatbase();

            var Area = new MoEObjectRepository(context).GetAreaById(context.Areas.First(e => e.AreaName == "area 1").AreaId);

            Assert.AreNotEqual(Area.Relationships.ExitRelationships.Count, 0);

        }
    }
}
