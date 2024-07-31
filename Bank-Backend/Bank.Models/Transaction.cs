public class Transaction
{
    int Id { get; set; }
    Account AccountId { get; set; }
    decimal Amount { get; set; }
    DateTime Time { get; set; }
}