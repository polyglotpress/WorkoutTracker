using Microsoft.EntityFrameworkCore;
using EFCoreWorkoutTrackerApp.Data;


public static class TestDbContextFactory
{
    public static DbContextOptions<AppDbContext> Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        return options;
    }
}