class Account
{
    int Id { get; set; }
    decimal Balance { get; set; }
    AccountType Type { get; set; }
    [ForeignKey]
    int PrimaryUser { get; set; }

}