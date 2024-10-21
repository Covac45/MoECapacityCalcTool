using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoECapacityCalc.Exits;
using MoECapacityCalc.Stairs;
using MoECapacityCalc.Utilities.Associations;
using MoECapacityCalc.Utilities.Datastructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database
{
    public interface IMoEDbContext
    {
        public DbSet<Exit> Exits { get; set; }
        public DbSet<Stair> Stairs { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
    }

    public class MoEContext : DbContext, IMoEDbContext
    {
        public DbSet<Exit> Exits { get; set; }
        public DbSet<Stair> Stairs { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        public MoEContext() { }

        public MoEContext(DbContextOptions<MoEContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(
                    $"Server=(localdb)\\mssqllocaldb; Database=MoECapacity; Trusted_Connection=True")
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exit>().HasKey(e => e.ExitId);
            modelBuilder.Entity<Stair>().Ignore(s => s.Relationships).HasKey(s => s.StairId);
            modelBuilder.Entity<Relationship>().HasKey(a => a.RelationshipId);

            //modelBuilder.Entity<Exit>().HasMany(r => r.Relationships);
            //modelBuilder.Entity<Stair>().HasMany(r => r.Relationships);

        }


}
}
