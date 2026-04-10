# Entity Framework Core Workout Tracker #

This is a basic C# .NET console app with Entity Framework Core and MySql.

- Create and save users and workouts in a MySQL database
- Interact with the database via a cleanly structured service layer


## Project Structure: ##
```
EFCoreExample/
├─ Models/
│  ├─ User.cs         # User entity
│  └─ Exercise.cs         # Exercise entity
├─ Data/
│  ├─ AppDbContext.cs        # EF Core DbContext
│  └─ AppDbContextFactory.cs # Design-time DbContext factory
├─ Services/
│  └─ UserService.cs  # Methods to interact with Users and Exercises
├─ Program.cs         # Main console app logic
├─ appsettings.json   # Database connection string
└─ Migrations/        # EF Core migrations

```

## Features ##
- Add users with name and email
- Add multiple posts per user
- Display user post counts
- Use service classes to separate database logic from Program.cs
- Fully migration-friendly with EF Core
- Prepared for easy upgrade to a web app / full-stack app

## Prerequisites ##
- .NET 8 SDK
- MySQL 8+
- MySQL root user

## Instructions to Run it ##

1. Clone the project:

` git clone https://github.com/polyglotpress/EFCoreWorkoutTracker.git `

2. Open the project in VSCode and in the terminal paste:
`cd EFCoreWorkoutTracker`

3. Create an appsettings.json file and paste the following:

```
{
    "Connectionstrings": {
        "DefaultConnection": "server=localhost;port=3306;database=yourdbname;user=root;password=yourpassword;"
    }
}

```
Swap "yourdbname" and "yourpassword" placeholders with your MySql database and user password.

4. In your terminal paste `dotnet restore`and press enter. This will install all packages from the .csproj file necessary to run the console app.

5. Run migrations `dotnet ef database update`

6. Run the console app `dotnet run`

7. Follow prompts in your terminal console.