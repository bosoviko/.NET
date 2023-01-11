using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BoardComputer> BoardComputers { get; set; }
        public DbSet<Command> Commands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BoardComputer>()
                .HasMany(f => f.Commands)
                .WithOne(f => f.BoardComputers!)
                .HasForeignKey(f => f.BoardComputerId);

            modelBuilder
                .Entity<Command>()
                .HasOne(c => c.BoardComputers)
                .WithMany(c => c.Commands)
                .HasForeignKey(c => c.BoardComputerId);
        }
    }
}
