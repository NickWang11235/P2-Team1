namespace BankBackend.Repository;

using Microsoft.EntityFrameworkCore;

using BankBackend.Models;

/// <summary>
/// 
/// </summary>
public class BankRepository : IBankRepository
{

    private BankContext _bankContext;

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
    /// <returns>the user with the Id if such user exists, null if not</returns>
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
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
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
    /// <returns>the user that is just created</returns>
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
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
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
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
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
    /// adds <c>account</c> to the account list of the user with <c>userId</c>
    /// </summary>
    /// <param name="account"></param>
    /// <param name="userId"></param>
    /// <returns>the updated user with the added account, null if the user does not exist</returns>
    public User? AddAccountToUser(Account account, int userId)
    {
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
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
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
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

    /// <summary>
    /// deletes the account with <c>accountId</c> from the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountId"></param>
    /// <returns>the account that was just deleted, null if user or account does not exist</returns>
    public Account? DeleteUserAccountByAccountId(int userId, int accountId)
    {
        User? user = _bankContext.Users.FirstOrDefault(x => x.UserId == userId);
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
    /// <param name="accountId"></param>
    /// <param name="balance"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Account UpdateBalance(int accountId, double balance)
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
    public Account UpdatePrimaryUser(int accountId, int userId)
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