using BoardComputerMiroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardComputerMiroservice.Data
{
    public class AppDdContext : DbContext
    {
        public DbSet<BoardComputer> BoardComputers { get; set; }

        public AppDdContext(DbContextOptions<AppDdContext> options) : base(options) 
        {
            
        }
    }
}
