namespace BankBackend.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    public int AccountId { get; set; }
    public double Balance { get; set; }
    public AccountType Type { get; set; }
    [ForeignKey(nameof(User.UserId))]
    public int PrimaryUserId { get; set; }
    public List<User> Users { get; set; }
    public Account()
    {
        Users = new List<User>();
    }
}