using EFCoreWorkoutTrackerApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWorkoutTrackerApp.Configuration;

public static class Config {


    public static DbContextOptions<AppDbContext> GetOptions()
    {


        var configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString));
        return optionsBuilder.Options;
    }
 }