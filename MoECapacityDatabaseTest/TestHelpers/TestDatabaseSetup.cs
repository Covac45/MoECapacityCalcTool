using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Database.Context;
using MoECapacityCalc.Database.Data_Logic.Repositories.RepositoryServices;
using MoECapacityCalc.Database.Data_Logic.Repositories;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.DomainEntities.Datastructs;

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

            Exit storeyExit1 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 1", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
            Exit finalExit1 = new Exit { Id = Guid.NewGuid(), Name = "final exit 1", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

            Exit storeyExit2 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 2", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
            Exit finalExit2 = new Exit { Id = Guid.NewGuid(), Name = "final exit 2", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };

            Exit storeyExit3 = new Exit { Id = Guid.NewGuid(), Name = "storey exit 3", ExitType = ExitType.storeyExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };
            Exit finalExit3 = new Exit { Id = Guid.NewGuid(), Name = "final exit 3", ExitType = ExitType.finalExit, DoorSwing = DoorSwing.with, ExitWidth = 1050 };


            Stair stair1 = new Stair
            {
                Id = Guid.NewGuid(),
                Name = "stair 1",
                StairWidth = 1000,
                FloorsServedPerEvacuationPhase = 3,
                IsSmokeProtected = false,
                FinalExitLevel = 0
            };

            Stair stair2 = new Stair
            {
                Id = Guid.NewGuid(),
                Name = "stair 2",
                StairWidth = 1100,
                FloorsServedPerEvacuationPhase = 3,
                IsSmokeProtected = false,
                FinalExitLevel = 0
            };

            Area area1 = new Area
            {
                Id = Guid.NewGuid(),
                Name = "area 1"
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

        public static Repositories GetRepositories()
        {
            var context = GetContext();
            var associationsRepository = new AssociationsRepository(context);
            IRelationshipSetBuildService<Exit> relationshipSetBuilderServiceExit = new RelationshipSetBuildService<Exit>(context, associationsRepository);
            IRelationshipSetBuildService<Stair> relationshipSetBuilderServiceStair = new RelationshipSetBuildService<Stair>(context, associationsRepository);
            IRelationshipSetBuildService<Area> relationshipSetBuilderServiceArea = new RelationshipSetBuildService<Area>(context, associationsRepository);

            var exitRepository = new ExitsRepository(context, relationshipSetBuilderServiceExit, associationsRepository);
            var stairRepository = new StairsRepository(context, relationshipSetBuilderServiceStair, associationsRepository);
            var areaRepository = new AreasRepository(context, relationshipSetBuilderServiceArea, associationsRepository);

            return new Repositories()
            {
                ExitsRepository = exitRepository,
                StairsRepository = stairRepository,
                AreasRepository = areaRepository,
                AssociationsRepository = associationsRepository
            };

        }




    }

}
