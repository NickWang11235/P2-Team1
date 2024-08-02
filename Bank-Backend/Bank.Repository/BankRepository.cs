using Microsoft.EntityFrameworkCore;

public class BankRepository : IBankRepository
{

    private BankContext _bankContext;

    public BankRepository(string connectionString)
    {
        DbContextOptions<BankContext> options;
        options = new DbContextOptionsBuilder<BankContext>()
            .UseSqlServer(connectionString)
            .Options;
        _bankContext = new BankContext(options);
    }

    public Account? GetAccountByAccountId(int accountId)
    {
        throw new NotImplementedException();
    }

    public List<Account> GetAccountsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public List<Account> GetAllAccounts()
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetAllTransactions()
    {
        throw new NotImplementedException();
    }

    public List<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public List<Account> GetPrimaryAccountsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Transaction? GetTransactionByTransactionId(int transactionId)
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetTransactionsByFromAccount(int fromAccountId)
    {
        throw new NotImplementedException();
    }

    public List<Transaction> GetTransactionsByToAccountId(int toAccountId)
    {
        throw new NotImplementedException();
    }

    public User? GetUserByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsersByAccountId(int accountId)
    {
        throw new NotImplementedException();
    }
}