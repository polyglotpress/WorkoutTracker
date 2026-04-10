

namespace EFCoreWorkoutTrackerApp.Models;


public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int Age { get; set; }

    public List<Exercise> Exercises { get; set; } = new List<Exercise>();

    public int ExerciseCount => Exercises.Count;
}