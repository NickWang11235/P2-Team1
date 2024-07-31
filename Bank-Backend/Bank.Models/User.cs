
using System.ComponentModel.DataAnnotations;

class User
{
    [Key]
    int Id { get; set; }
    string Password { get; set; }
    string Name { get; set; }
    List<Account> Accounts { get; set; }

}