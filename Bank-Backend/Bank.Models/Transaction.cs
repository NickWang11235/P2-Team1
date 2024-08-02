using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    public Account FromAccount { get; set; }
    public Account? ToAccount { get; set; }
    public double Amount { get; set; }
    public DateTime Time { get; set; }

    public Transaction()
    {
        FromAccount = new Account();
        Time = DateTime.Now;
    }

    public Transaction(Account account, double amount)
    {
        FromAccount = account;
        Amount = amount;
        Time = DateTime.Now;
    }

    public Transaction(Account fromAccount, Account toAccount, double amount)
    {
        FromAccount = fromAccount;
        ToAccount = toAccount;
        Amount = amount;
        Time = DateTime.Now;
    }

}