namespace BankBackend.Repository;

using Microsoft.EntityFrameworkCore;

using BankBackend.Models;

/// <summary>
/// 
/// </summary>
public class BankRepository : IBankRepository
{

    private readonly BankContext _bankContext;

    /// <summary>
    /// constructor for dependency injection
    /// </summary>
    /// <param name="bankContext"></param>
    public BankRepository(BankContext bankContext)
    {
        _bankContext = bankContext;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionString"></param>
    public BankRepository(string connectionString)
    {
        DbContextOptions<BankContext> options;
        options = new DbContextOptionsBuilder<BankContext>()
            .UseSqlServer(connectionString)
            .Options;
        _bankContext = new BankContext(options);
    }


    /// <summary>
    /// find a user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>the user with the Id, null if user does not exist</returns>
    public User? GetUserByUserId(int userId)
    {
        return _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
    }

    /// <summary>
    /// find all existing users in the database
    /// </summary>
    /// <returns>a list containing all existing users, empty list if non exists</returns>
    public List<User> GetAllUsers()
    {
        return _bankContext.Users.ToList();
    }

    /// <summary>
    /// find all the accounts of the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>a list containing all the accounts, null if user does not exist</returns>
    public List<Account>? GetAccountsByUserId(int userId)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            return user.Accounts;
        }
    }

    /// <summary>
    /// uploads the user to database
    /// </summary>
    /// <param name="user"></param>
    /// <returns>the user that was just created</returns>
    public User CreateUser(User user)
    {
        User newUser = _bankContext.Users.Add(user).Entity;
        _bankContext.SaveChanges();
        return newUser;
    }

    /// <summary>
    /// updates the password of user with <c>userId</c> to <c>password</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns>the updated user with the new password, null if the user does not exist</returns>
    public User? UpdatePassword(int userId, string password)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            user.Password = password;
            _bankContext.SaveChanges();
            return user;
        }
    }

    /// <summary>
    /// updates the name of the user with <c>userId</c> to <c>name</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <returns>the updated user with the new name, null if the user does not exist</returns>
    public User? UpdateName(int userId, string name)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            user.Name = name;
            _bankContext.SaveChanges();
            return user;
        }
    }

    /// <summary>
    /// adds account with <c>accountId</c> to the account list of the user with <c>userId</c>
    /// </summary>
    /// <param name="account"></param>
    /// <param name="userId"></param>
    /// <returns>the updated user with the added account, null if the user does not exist</returns>
    public User? AddAccountToUser(Account account, int userId)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            user.Accounts.Add(account);
            _bankContext.SaveChanges();
            return user;
        }
    }

    /// <summary>
    /// deletes the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>the user that was just deleted, null if the user does not exist</returns>
    public User? DeleteUserById(int userId)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            _bankContext.Users.Remove(user);
            _bankContext.SaveChanges();
            return user;
        }
    }


    // THIS IS POTENTIALLY PROBLEMATIC. REQURE TESTING!!!

    /// <summary>
    /// deletes the account with <c>accountId</c> from the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountId"></param>
    /// <returns>the account that was just deleted, null if user does not exist, or the user does not have an account with <c>accountId</c></returns>
    public Account? DeleteUserAccountByAccountId(int userId, int accountId)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            Account? account = user.Accounts.FirstOrDefault(x => x.AccountId == accountId);
            if (account == null)
            {
                return null;
            }
            else
            {
                user.Accounts.Remove(account);
                _bankContext.SaveChanges();
                return account;
            }
        }
    }



    /// <summary>
    /// find an account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>the account with <c>accountId</c>, null if account does not exist</returns>
    public Account? GetAccountByAccountId(int accountId)
    {
        return _bankContext.Accounts.FirstOrDefault(x => x.AccountId == accountId);
    }

    /// <summary>
    /// find all existing accounts in the database
    /// </summary>
    /// <returns>list containing all existing accounts, empty list if non exists</returns>
    public List<Account> GetAllAccounts()
    {
        return _bankContext.Accounts.ToList();
    }

    /// <summary>
    /// find all the users of the account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>a list containing all the users, null if account does not exist</returns>
    public List<User>? GetUsersByAccountId(int accountId)
    {
        Account? account = GetAccountByAccountId(accountId);
        if (account == null)
        {
            return null;
        }
        else
        {
            return account.Users;
        }
    }

    /// <summary>
    /// find all the accounts that the user with <c>accountId</c> is a primary user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>a list containing all the primary accounts, null if user does not exist</returns>
    public List<Account>? GetPrimaryAccountsByUserId(int userId)
    {
        User? user = GetUserByUserId(userId);
        if (user == null)
        {
            return null;
        }
        else
        {
            return _bankContext.Accounts.Where(x => x.PrimaryUserId == userId).ToList();
        }
    }

    /// <summary>
    /// uploads the account to database
    /// </summary>
    /// <param name="account"></param>
    /// <returns>the account that was just created</returns>
    public Account CreateAccount(Account account)
    {
        Account newAccount = _bankContext.Accounts.Add(account).Entity;
        _bankContext.SaveChanges();
        return newAccount;
    }

    /// <summary>
    ///  updates the balance of the account with <c>accountId</c> to <c>balance</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="balance"></param>
    /// <returns>the updated account with the new balance, null if the account does not exist</returns>
    public Account? UpdateBalance(int accountId, double balance)
    {
        Account? account = GetAccountByAccountId(accountId);
        if (account == null)
        {
            return null;
        }
        else
        {
            account.Balance = balance;
            _bankContext.SaveChanges();
            return account;
        }
    }

    /// <summary>
    /// replaces the primary user of the account with <c>accountId</c> with <c>userId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="userId"></param>
    /// <returns>the updated account with the new primary user, null if account doesnot exist or if user does not exist</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Account? UpdatePrimaryUser(int accountId, int userId)
    {
        Account? account = GetAccountByAccountId(accountId);
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
        if (user == null || account == null)
        {
            return null;
        }
        else
        {
            account.PrimaryUserId = userId;
            _bankContext.SaveChanges();
            return account;
        }
    }

    /// <summary>
    /// adds user with <c>userId</c> to the account list of the account with <c>accountId</c>
    /// </summary>
    /// <param name="user"></param>
    /// <param name="accountId"></param>
    /// <returns>the updated account with the new user, null if account does not exist or if user does not exist</returns>
    public Account? AddUserToAccount(User user, int accountId)
    {
        Account? account = GetAccountByAccountId(accountId);
        if (account == null || user == null)
        {
            return null;
        }
        else
        {
            account.Users.Add(user);
            _bankContext.SaveChanges();
            return account;
        }
    }

    /// <summary>
    /// deletes the account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>the account that was just deleted, null if the account does not exist</returns>
    public Account? DeleteAccountById(int accountId)
    {
        Account? account = GetAccountByAccountId(accountId);
        if (account == null)
        {
            return null;
        }
        else
        {
            _bankContext.Accounts.Remove(account);
            _bankContext.SaveChanges();
            return account;
        }
    }


    // THIS IS POTENTIALLY PROBLEMATIC. REQURE TESTING!!!

    /// <summary>
    /// deletes the user with <c>userId</c> from the account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="userId"></param>
    /// <returns>the user that was just deleted, null if the account does not exist, or if the account does not have an user with <c>userId</c></returns>
    public User? DeleteAccountUserByUserId(int accountId, int userId)
    {
        Account? account = GetAccountByAccountId(accountId);
        if (account == null)
        {
            return null;
        }
        else
        {
            User? user = account.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                return null;
            }
            else
            {
                account.Users.Remove(user);
                _bankContext.SaveChanges();
                return user;
            }
        }

    }



    /// <summary>
    /// find a transaction with <c>transactionId</c>
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns>the transaction with <c>transactionId</c>, null if transaction does not exist</returns>
    public Transaction? GetTransactionByTransactionId(int transactionId)
    {
        return _bankContext.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);
    }

    /// <summary>
    /// find all existing transactions in the database
    /// </summary>
    /// <returns>list containing all existing transactions, empty list if non exists</returns>
    public List<Transaction> GetAllTransactions()
    {
        return _bankContext.Transactions.ToList();
    }

    /// <summary>
    /// find all transaction with <c><fromAccountId/c> as the from account
    /// </summary>
    /// <param name="fromAccountId"></param>
    /// <returns>list containing all such transactions</returns>
    public List<Transaction> GetTransactionsByFromAccountId(int fromAccountId)
    {
        return _bankContext.Transactions.Where(x => x.FromAccount.AccountId == fromAccountId).ToList();
    }

    /// <summary>
    /// find all transaction with <c><toAccountId/c> as the from account
    /// </summary>
    /// <param name="toAccountId"></param>
    /// <returns>list containing all such transactions</returns>
    public List<Transaction> GetTransactionsByToAccountId(int toAccountId)
    {
        return _bankContext.Transactions.Where(x => x.ToAccount != null && x.ToAccount.AccountId == toAccountId).ToList();
    }

    /// <summary>
    /// uploads the transaction to database
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns>the transaction that was just created</returns>
    public Transaction CreateTransaction(Transaction transaction)
    {
        Transaction newTransaction = _bankContext.Transactions.Add(transaction).Entity;
        _bankContext.SaveChanges();
        return newTransaction;
    }

    /// <summary> 
    /// deletes the transaction with <c>transactionId</c>
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns>the transaction that was just deleted, null if the transaction does not exist</returns>
    public Transaction? DeleteTransactionByTransactionId(int transactionId)
    {
        Transaction? transaction = GetTransactionByTransactionId(transactionId);
        if (transaction == null)
        {
            return null;
        }
        else
        {
            _bankContext.Transactions.Remove(transaction);
            _bankContext.SaveChanges();
            return transaction;
        }
    }

}