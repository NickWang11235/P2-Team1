namespace BankBackend.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    // list of all accounts this user has access to
    public List<Account> Accounts { get; set; }

    public User()
    {
        Username = "";
        Password = "";
        Name = "";
        Accounts = new List<Account>();
    }

    public User(string username, string password, string name)
    {
        Username = username;
        Password = password;
        Name = name;
        Accounts = new List<Account>();
    }

}