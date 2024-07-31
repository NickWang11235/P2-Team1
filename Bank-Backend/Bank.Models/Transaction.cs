using System.ComponentModel.DataAnnotations;

public class Transaction
{
    [Key]
    int Id { get; set; }
    Account FromAccount { get; set; }
    Account? ToAccount { get; set; }
    decimal Amount { get; set; }
    DateTime Time { get; set; }

    public Transaction()
    {
        FromAccount = new Account();
        Time = DateTime.Now;
    }

    public Transaction(Account account, decimal amount)
    {
        FromAccount = account;
        Amount = amount;
        Time = DateTime.Now;
    }

    public Transaction(Account fromAccount, Account toAccount, decimal amount)
    {
        FromAccount = fromAccount;
        ToAccount = toAccount;
        Amount = amount;
        Time = DateTime.Now;
    }

}