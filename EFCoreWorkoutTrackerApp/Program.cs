using EFCoreWorkoutTrackerApp.Data;
using EFCoreWorkoutTrackerApp.Services;
using EFCoreWorkoutTrackerApp.Seeds;
using EFCoreWorkoutTrackerApp.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWorkoutTrackerApp;

class Program
{
    static void Main(string[] args)
    {

        try
        {


            //MySQL database connection setup
            var options = Config.GetOptions();
            using var db = new AppDbContext(options);

            db.Database.Migrate(); //
            SeedData.Seed(db); //seed database

            var userService = new UserService(db);
            var exerciseService = new ExerciseService(db);

            var app = new App(db, userService, exerciseService);
            app.Start();
        } catch (Exception ex)
        {
            Console.WriteLine("Error! ", ex.Message);
        }

      
        Console.WriteLine("Thank you for visiting the Workout Tracker. See you soon!");
    }
}
