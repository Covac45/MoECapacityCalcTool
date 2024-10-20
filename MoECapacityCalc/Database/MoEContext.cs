using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
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
    public class MoEContext : DbContext
    {
        public DbSet<Exit> Exits { get; set; }
        public DbSet<Stair> Stairs { get; set; }
        //public DbSet<Association> Associations { get; set; }

        public MoEContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                $"Server=(localdb)\\mssqllocaldb; Database=MoECapacity; Trusted_Connection=True")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exit>().HasKey(e => e.ExitId);
            modelBuilder.Entity<Stair>().HasKey(s => s.StairId);
            modelBuilder.Entity<Association>().HasKey(a => a.AssociationId);

            modelBuilder.Entity<Association>().HasMany(a => a.Exits);
            modelBuilder.Entity<Association>().HasMany(a => a.Stairs);

        }

}
}
