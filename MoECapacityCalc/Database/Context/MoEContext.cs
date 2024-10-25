using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoECapacityCalc.DomainEntities;
using MoECapacityCalc.Utilities.Associations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoECapacityCalc.Database.Context
{
    public interface IMoEDbContext
    {
        public DbSet<Exit> Exits { get; set; }
        public DbSet<Stair> Stairs { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Association> Associations { get; set; }
    }

    public class MoEContext : DbContext, IMoEDbContext
    {
        public DbSet<Exit> Exits { get; set; }
        public DbSet<Stair> Stairs { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Association> Associations { get; set; }

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
            modelBuilder.Entity<Exit>().Ignore(e => e.Relationships).HasKey(e => e.Id);
            modelBuilder.Entity<Stair>().Ignore(s => s.Relationships).HasKey(s => s.Id);
            modelBuilder.Entity<Area>().Ignore(a => a.Relationships).HasKey(a => a.Id);
            modelBuilder.Entity<Association>().HasKey(a => a.AssociationId);

        }


    }
}
