
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    int Id { get; set; }
    string Password { get; set; }
    string Name { get; set; }
    List<Account> Accounts { get; set; }

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