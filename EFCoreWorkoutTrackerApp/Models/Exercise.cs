namespace EFCoreWorkoutTrackerApp.Models;

public class Exercise
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public bool Completed { get; set; }

    //foreign key
    public int UserId { get; set; }

    //navigation
    public User? User { get; set; }
}