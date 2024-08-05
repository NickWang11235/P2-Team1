namespace BankBackend.Models;

using System.ComponentModel.DataAnnotations;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    public Account FromAccount { get; private set; }
    public Account? ToAccount { get; private set; }
    public double Amount { get; private set; }
    public DateTime Time { get; private set; }

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