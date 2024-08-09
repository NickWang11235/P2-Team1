namespace BankBackend.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    public int AccountId { get; set; }
    public double Balance { get; set; }
    // type of account such as Savings or Checkings
    public AccountType Type { get; set; }
    [ForeignKey(nameof(User.UserId))]
    // id of a "primary user" that act as the owner/admin of this account
    // this user has the premission to add/remove users from accounts
    public int PrimaryUserId { get; set; }
    // list of all users including the primary user
    public List<User> Users { get; set; }
    public Account()
    {
        Users = new List<User>();
    }
}