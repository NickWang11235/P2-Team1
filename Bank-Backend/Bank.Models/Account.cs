using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    int Id { get; set; }
    decimal Balance { get; set; }
    AccountType Type { get; set; }
    User PrimaryUser { get; set; }
    List<User> Users { get; set; }

    public Account()
    {
        PrimaryUser = new User();
        Users = new List<User>();
    }

    public Account(User user)
    {
        PrimaryUser = user;
        Users = new List<User>();
    }

}