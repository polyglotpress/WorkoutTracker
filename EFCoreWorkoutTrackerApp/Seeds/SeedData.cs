using EFCoreWorkoutTrackerApp.Data;
using EFCoreWorkoutTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWorkoutTrackerApp.Seeds;

public static class SeedData
{
    public static void Seed(AppDbContext db)
    {
        //  db.Exercises.RemoveRange(db.Exercises);
        // db.Users.RemoveRange(db.Users);
        db.Database.ExecuteSqlRaw("DELETE FROM Exercises;");
        db.Database.ExecuteSqlRaw("DELETE FROM Users;");

        db.Database.ExecuteSqlRaw("ALTER TABLE Users AUTO_INCREMENT = 1;");
        db.Database.ExecuteSqlRaw("ALTER TABLE Exercises AUTO_INCREMENT = 1;");

        var users = new List<User>
        {
            new User {Name = "Tim", Email = "tim@gmail.com", Age= 45, Exercises= new List<Exercise>
            {
               new Exercise {Name="Squats", Description="", Duration=5, Completed=false},
            new Exercise {Name="Running", Description="", Duration=15, Completed=true}
            }
            },
            new User {Name = "Becca", Email = "rebecca@gmail.com", Age= 27, Exercises = new List<Exercise>
            {
               new Exercise {Name="Push Ups", Description="", Duration=7, Completed=false}
            }
            },
            new User {Name = "Josh", Email = "josh@gmail.com", Age= 32,Exercises = new List<Exercise>
            {

            new Exercise {Name="Abs", Description="", Duration=10, Completed=false},
            new Exercise {Name="Plank", Description="", Duration=2, Completed=false}
            } }

        };
        db.Users.AddRange(users);
        db.SaveChanges();


    }
}