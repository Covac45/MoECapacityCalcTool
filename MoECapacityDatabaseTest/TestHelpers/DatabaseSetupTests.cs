using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityDatabaseTest.TestHelpers
{
    [TestClass]
    public class DatabaseSetupTests : TestDatabaseSetup
    {
        [TestMethod]

        public void SeedDatbaseTest()
        {
            var context = GetContext();
            ResetDatbase();
            SeedDatbase();

            var exits = context.Exits.Select(e => e.ExitId).ToList().Count();

            Assert.AreNotEqual(context.Exits.Select(e => e.ExitId).ToList().Count(),0);
            Assert.AreNotEqual(context.Stairs.Select(e => e.StairId).ToList().Count(),0);
            Assert.AreNotEqual(context.Relationships.Select(e => e.RelationshipId).ToList().Count(), 0);
        }
            
    }
}
