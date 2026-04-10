using EFCoreWorkoutTrackerApp.Data;
using EFCoreWorkoutTrackerApp.Services;

public class App
{
    private readonly AppDbContext _db;
    private readonly UserService _userService;
    private readonly ExerciseService _exerciseService;

    private int menuChoice;

    public App(AppDbContext db, UserService userService, ExerciseService exerciseService)
    {
        _db = db;
        _userService = userService;
        _exerciseService = exerciseService;
    }

    public void Start()
    {
        menuChoice = 1;
        while (menuChoice != 6) ShowMenu();
    }

    private void ShowMenu()
    {
        Console.WriteLine("MENU:");
        Console.WriteLine("1: Create account");
        Console.WriteLine("2: Add exercise");
        Console.WriteLine("3: List all exercises");
        Console.WriteLine("4: User profile");
        Console.WriteLine("5: Mark exercise complete");
        Console.WriteLine("6: Exit");
        Console.WriteLine("Enter option:");

        menuChoice = int.Parse(Console.ReadLine()!);
        SwitchMenu(menuChoice);
    }

    private void SwitchMenu(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.WriteLine("Create User");
               TryCatch(() => CreateUser());
                break;
            case 2:
                Console.WriteLine("Add exercise");
               TryCatch(() => AddExercise());
                break;
            case 3:
                Console.WriteLine("List all exercises");
                TryCatch(() => GetExercises());
                break;
            case 4:
                Console.WriteLine("User Profile");
                TryCatch(()=> ViewUserProfile());
                break;
            case 5:
                Console.WriteLine("Mark complete");
                TryCatch(() => MarkExerciseComplete());
                break;
            case 6:
                break;
            default:
                Console.WriteLine("Unavailable menu option");
                break;
        }
    }

    public static void TryCatch(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error! " + ex.Message);
        }
    }

    private void CreateUser()
    {   
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine()!;
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine()!;

            Console.WriteLine("Enter your age:");
            int age = int.Parse(Console.ReadLine()!);

            var user = _userService.CreateUser(name, email, age);

            Console.WriteLine($"Saved new user {user.Id}");
    }

    private void AddExercise()
    {
        //create exercise
        Console.WriteLine("Enter your user Id");
         
         if(!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }

        var user = _db.Users.Find(userId)!;
        
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Console.WriteLine("Insert Exercise Name:");
        string? name = Console.ReadLine();
        if (name == null)
        {
            Console.WriteLine("Name cannot be empty");
            return;
        }

        Console.WriteLine("Insert Exercise Details:");
        string? description = Console.ReadLine();

        Console.WriteLine("How many minutes is this exercise:");
       
        if (!int.TryParse(Console.ReadLine(), out int duration))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        Console.WriteLine("Have you completed the exercise yet? Enter 'yes' or 'no' ");
        bool completed = Console.ReadLine() == "yes" ? true : false;
        _exerciseService.AddExercise(user, name, description, duration, completed);
        Console.WriteLine($"User {user.Name}, you've successfully saved a new exercise.");
    }

    private void GetExercises()
    {
        var exercises = _exerciseService.GetExercises();
        foreach (var e in exercises)
        {
            var user = _userService.GetUserById(e.UserId)!;
            Console.WriteLine($"ID: {e.Id}, Name: {e.Name}, {e.Description}, {e.Duration} minutes, completed {e.Completed}, by {user.Name}");
        } //add user name
        Console.WriteLine("Choose an id to edit or delete");
        int id = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Press 1 to edit or 2 to delete");
        int choice = int.Parse(Console.ReadLine()!);

        if (choice == 1)
        {
            var exercise = _exerciseService.GetExerciseById(id);
            if(exercise == null)
            {
                Console.WriteLine("Exercise not found");
                return;
            }
            Console.WriteLine("Edit name?");
            string? name = Console.ReadLine();
            if (name == "" || name == null) name = exercise.Name;

            Console.WriteLine("Edit description?");
            string? inputDescription = Console.ReadLine();
            string description = string.IsNullOrEmpty(inputDescription)
            ? (exercise.Description ?? "") //???
            : inputDescription;
            

            Console.WriteLine("Edit duration?");
            string? input = Console.ReadLine();

            int duration = int.TryParse(input, out int number)
            ? number : exercise.Duration;


            Console.WriteLine("Edit completion state?");
            string completedEdit = Console.ReadLine()!;
            bool completed;

            if (string.IsNullOrWhiteSpace(completedEdit))
            {
                completed = exercise.Completed;
            }
else if(!bool.TryParse(completedEdit, out completed))
            {
                Console.WriteLine("Invalid input");
                completed = exercise.Completed;
            }

           

            _exerciseService.EditExercise(id, name, description, duration, completed);
            Console.WriteLine("Exercise updated:");
            var updatedExercise = _exerciseService.GetExerciseById(id);
            if(updatedExercise == null)
            {
                Console.WriteLine("Exercise not found");
                return;
            }
            Console.WriteLine($"{updatedExercise.Id}, {updatedExercise.Name}, {updatedExercise.Description}, {updatedExercise.Duration}, {updatedExercise.Completed}");
        }
        else if (choice == 2)
        {
            _exerciseService.DeleteExercise(id);
            Console.WriteLine("Deleted successfully");
        }
    }

    private void ViewUserProfile()
    {
        Console.WriteLine("User profile, enter Id");
        int userId = int.Parse(Console.ReadLine()!);
        var userProfile = _userService.GetUserById(userId);
        if(userProfile == null)
        {
            Console.WriteLine("User not found.");
            return;
        }
        Console.WriteLine($"ID: {userProfile.Id}, {userProfile.Name}, {userProfile.Email}, age: {userProfile.Age}");
        Console.WriteLine("1. Edit Profile");
        Console.WriteLine("2. Delete Account");

         if(!int.TryParse(Console.ReadLine(), out int userChoice))
        {
            Console.WriteLine("Invalid choice");
            return;
        }
    
        if (userChoice == 1)
        {
            Console.WriteLine("Enter updated name");
            string? nameInput = Console.ReadLine();
            string updatedName = string.IsNullOrWhiteSpace(nameInput)
            ? (userProfile.Name ?? "")
            : nameInput;

            Console.WriteLine("Enter updated email");
            string? emailInput = Console.ReadLine();
            string updatedEmail = string.IsNullOrWhiteSpace(emailInput)
            ? (userProfile.Email ?? "")
            : emailInput;
            
            Console.WriteLine("Enter updated Age");
             if(!int.TryParse(Console.ReadLine(), out int updatedAge))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            _userService.UpdateUserDetails(userId, updatedName, updatedEmail, updatedAge);
            

        }
        else if (userChoice == 2)
        {
            _userService.DeleteUser(userId);
        }
    }

    private void MarkExerciseComplete()
    {
        Console.WriteLine("Insert exercise ID");

        if (!int.TryParse(Console.ReadLine(), out int exerciseId))
        {
            Console.WriteLine("Invalid exercise ID.");
            return;
        }
        bool markedComplete = _exerciseService.MarkExerciseComplete(exerciseId);

        if (!markedComplete)
        {
            Console.WriteLine("Exercise not found");
        }
         else
        {
            Console.WriteLine("Exercise marked complete!");
        }

    }

}