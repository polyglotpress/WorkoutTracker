using Microsoft.EntityFrameworkCore;
using EFCoreWorkoutTrackerApp.Models;


namespace EFCoreWorkoutTrackerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Exercise> Exercises => Set<Exercise>();
    }
}