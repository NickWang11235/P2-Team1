namespace BankBackend.Models;

using System.ComponentModel.DataAnnotations;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    // the account transaction originates from. a withdraw/deposite involves only the FromAccount
    public Account FromAccount { get; set; }
    // the account transaction is acting on, e.g. sending money to this account
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