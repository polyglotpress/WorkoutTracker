using EFCoreWorkoutTrackerApp.Data;
using EFCoreWorkoutTrackerApp.Models;
using EFCoreWorkoutTrackerApp.Services;

namespace EFCoreWorkoutTrackerTests;

public class AppTests
{
    [Fact]
    public void MarkExerciseComplete_ShouldReturnTrue()
    {
        var options = TestDbContextFactory.Create();

        using var db = new AppDbContext(options);

        var service = new ExerciseService(db);

        var exercise = new Exercise { Name = "Push up", Completed = false };
        db.Exercises.Add(exercise);
        db.SaveChanges();



        bool updated = service.MarkExerciseComplete(exercise.Id);
        var updatedExercise = db.Exercises.Find(exercise.Id)!;

        Assert.True(updated);
        Assert.True(updatedExercise.Completed);
    }
}
