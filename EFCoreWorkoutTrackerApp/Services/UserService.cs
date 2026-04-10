using EFCoreWorkoutTrackerApp.Data;
using EFCoreWorkoutTrackerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWorkoutTrackerApp.Services;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }


    public User CreateUser(string name, string email, int age)
    {
        var user = new User { Name = name, Email = email, Age = age };
        _db.Users.Add(user);
        _db.SaveChanges();
        return user;
    }

    //list users
    public List<User> ListUsers()
    {
        return _db.Users.ToList();
    }

    public User? GetUserById(int userId)
    {
        return _db.Users.FirstOrDefault(u => u.Id == userId);
    }

    //edit user details
    public void UpdateUserDetails(int userId, string name, string email, int age)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == userId);
         if (user == null)
            throw new Exception("User not found");
        user.Name = name;
        user.Email = email;
        user.Age = age;

        _db.SaveChanges();
    }
    
    //delete user

    public void DeleteUser(int userId)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == userId);
          if (user == null)
            throw new Exception("User not found");
        _db.Users.Remove(user);
        _db.SaveChanges();
    }

}