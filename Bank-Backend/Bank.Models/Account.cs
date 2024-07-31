class Account
{
    int Id { get; set; }
    decimal Balance { get; set; }
    AccountType Type { get; set; }
    User PrimaryUser { get; set; }
    List<User> Users { get; set; }

}