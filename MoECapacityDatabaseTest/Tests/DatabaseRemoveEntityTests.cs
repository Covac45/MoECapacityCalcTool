using MoECapacityCalc.DomainEntities.Datastructs;
using MoECapacityCalc.DomainEntities;
using MoECapacityDatabaseTest.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityDatabaseTest.Tests
{
    [TestClass]

    public class DatabaseRemoveEntityTests : TestDatabaseSetup
    {
        private Repositories _repositories = GetRepositories();

        [TestMethod]
        public void CanRemoveExitFromDatabase()
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
            }

        }

    }
}
