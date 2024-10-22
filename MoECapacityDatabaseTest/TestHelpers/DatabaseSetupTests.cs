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
            SeedDatbase();

            var exits = context.Exits.Select(e => e.Id).ToList().Count();

            Assert.AreNotEqual(context.Exits.Select(e => e.Id).ToList().Count(),0);
            Assert.AreNotEqual(context.Stairs.Select(e => e.Id).ToList().Count(),0);
            Assert.AreNotEqual(context.Associations.Select(e => e.AssociationId).ToList().Count(), 0);
        }
            
    }
}
