namespace BankBackend.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    // list of all accounts this user has access to
    public List<Account> Accounts { get; set; }

    public User()
    {
        Password = "";
        Name = "";
        Accounts = new List<Account>();
    }

    public User(string password, string name)
    {
        Password = password;
        Name = name;
        Accounts = new List<Account>();
    }

}