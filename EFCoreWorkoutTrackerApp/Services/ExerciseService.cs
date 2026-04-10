using EFCoreWorkoutTrackerApp.Data;
using EFCoreWorkoutTrackerApp.Models;

namespace EFCoreWorkoutTrackerApp.Services;

public class ExerciseService
{
    private readonly AppDbContext _db;

    public ExerciseService(AppDbContext db)
    {
        _db = db;
    }

    public List<Exercise> GetExercises()
    {
        return _db.Exercises.ToList();
    }

    public Exercise? GetExerciseById(int exerciseId)
    {
        return _db.Exercises.FirstOrDefault(e => e.Id == exerciseId);
    }

    public void AddExercise(User user, string name, string? description, int duration, bool completed)
    {
        var exercise = new Exercise { Name = name, Description = description, Duration = duration, Completed = completed, UserId = user.Id };

        user.Exercises.Add(exercise);

        _db.Exercises.Add(exercise);
        _db.SaveChanges();
    }

    public bool MarkExerciseComplete(int exerciseId) //add to menu
    {
        var exercise = _db.Exercises.Find(exerciseId);
        if(exercise == null)
        {
            return false;
        }
        exercise.Completed = true;
        _db.SaveChanges();

        return true;
    }

    public void EditExercise(int exerciseId, string name, string description, int duration, bool completed)
    {
        var exercise = _db.Exercises.FirstOrDefault(e => e.Id == exerciseId);
        if (exercise == null)
            throw new Exception("Exercise not found");
       
        exercise.Name = name;
        exercise.Description = description;
        exercise.Duration = duration;
        exercise.Completed = completed;

        _db.SaveChanges();
    }

    public void DeleteExercise(int exerciseId)
    {
        var exercise = _db.Exercises.FirstOrDefault(e => e.Id == exerciseId);
 if (exercise == null)
            throw new Exception("Exercise not found");
        _db.Exercises.Remove(exercise);
        _db.SaveChanges();
    }

}