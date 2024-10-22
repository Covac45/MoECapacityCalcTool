using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Utilities.Datastructs;
using System.Reflection.Emit;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Areas;

namespace MoECapacityDatabaseTest.TestHelpers
{
    public class TestDatabaseSetup
    {

        public static MoEContext GetContext()
        {
            var builder = new DbContextOptionsBuilder<MoEContext>();

            builder.UseSqlServer(
                $"Server=(localdb)\\mssqllocaldb; Database=MoECapacityTest; Trusted_Connection=True")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information);

            var context = new MoEContext(builder.Options);

                return context;
        }


        public static void ResetDatbase()
        {
            using var context = GetContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }

        public static void SeedDatbase()
        {
            ResetDatbase();

            Exit storeyExit1 = new Exit { Id = Guid.NewGuid(), ExitName = "storey exit 1", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
            Exit finalExit1 = new Exit { Id = Guid.NewGuid(), ExitName = "final exit 1", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

            Exit storeyExit2 = new Exit { Id = Guid.NewGuid(), ExitName = "storey exit 2", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
            Exit finalExit2 = new Exit { Id = Guid.NewGuid(), ExitName = "final exit 2", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

            Exit storeyExit3 = new Exit { Id = Guid.NewGuid(), ExitName = "storey exit 3", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
            Exit finalExit3 = new Exit { Id = Guid.NewGuid(), ExitName = "final exit 3", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };


            Stair stair1 = new Stair
            {
                Id = Guid.NewGuid(),
                StairName = "stair 1",
                StairWidth = 1000,
                FloorsServed = 3,
                FinalExitLevel = 0
            };

            Stair stair2 = new Stair
            {
                Id = Guid.NewGuid(),
                StairName = "stair 2",
                StairWidth = 1100,
                FloorsServed = 3,
                FinalExitLevel = 0
            };

            Area area1 = new Area
            {
                Id = Guid.NewGuid(),
                AreaName = "area 1"
            };

            using (var context = GetContext())
            {
                context.Exits.AddRange(
                storeyExit1,
                finalExit1,
                storeyExit2,
                finalExit2,
                storeyExit3,
                finalExit3);

                context.Stairs.AddRange(stair1, stair2);

                context.Associations.AddRange(
                    new Association(stair1, storeyExit1),
                    new Association(stair1, finalExit1),
                    new Association(stair2, storeyExit2),
                    new Association(stair2, finalExit2),
                    new Association(storeyExit3, finalExit3));

                context.Areas.Add(area1);

                context.Associations.AddRange(
                    new Association(area1, storeyExit3),
                    new Association(area1, finalExit3),
                    new Association(area1, stair1),
                    new Association(area1, stair2));

                context.SaveChanges();
            }
        }

    }
}
