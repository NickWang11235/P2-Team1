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


    /// <summary>
    /// find a user given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>the user with the Id if such user exists, null if not</returns>
    public User? GetUserByUserId(int userId)
    {
        return _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<User> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<User> GetUsersByAccountId(int accountId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public User CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public User AddAccountToUser(Account account, int userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public User DeleteUserById(int userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int DeleteUserAccountByAccountId(int userId, int accountId)
    {
        throw new NotImplementedException();
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public Account? GetAccountByAccountId(int accountId)
    {
        return _bankContext.Accounts.Where(x => x.AccountId == accountId).First();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Account> GetAllAccounts()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Account> GetAccountsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Account> GetPrimaryAccountsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Account CreateAccount(Account account)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="accountId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Account AddUserToAccount(User user, int accountId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Account DeleteAccountById(int accountId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int DeleteAccountUserByUserId(int accountId, int userId)
    {
        throw new NotImplementedException();
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Transaction? GetTransactionByTransactionId(int transactionId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Transaction> GetAllTransactions()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fromAccountId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Transaction> GetTransactionsByFromAccount(int fromAccountId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="toAccountId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Transaction> GetTransactionsByToAccountId(int toAccountId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Transaction CreateTransaction(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Transaction DeleteTransactionByTransactionId(int transactionId)
    {
        throw new NotImplementedException();
    }
}