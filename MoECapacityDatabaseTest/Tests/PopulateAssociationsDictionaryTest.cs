using MoECapacityCalc.Database;
using MoECapacityCalc.Utilities.Services;
using MoECapacityDatabaseTest.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]
    public class PopulateAssociationsDictionaryTest : TestDatabaseSetup
    {
        /*[TestMethod]
        public void CanTestDictionary()
        {
            using var context = GetContext();
            SeedDatbase();

            var exitDictionary = new AssociationsDictionary(context).CreateExitDictionary();
            var stairDictionary = new AssociationsDictionary(context).CreateStairDictionary();

            var exit = exitDictionary.First().Value;
            var stair = stairDictionary.OrderBy(s => s.Value.StairName).First().Value;


            Assert.AreEqual(exit.ExitWidth, 1050);
            Assert.AreEqual(stair.StairWidth, 1000);
            
        }*/
    }
}
