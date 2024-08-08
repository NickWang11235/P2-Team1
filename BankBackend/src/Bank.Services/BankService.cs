using BankBackend.Repository;
using BankBackend.Models;

namespace BankBackend.Service;

public class BankService : IBankService
{
    private readonly IBankRepository _bankRepository;

    public BankService(IBankRepository repository)
    {
        _bankRepository = repository;
    }

    public string ValidateLogin(string username, string password)
    {
        var user = _bankRepository.GetAllUsers().FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return "Username not found.";
        }

        if (user.Password != password)
        {
            return "Invalid Password.";
        }

        return "Login Successful.";
    }

    public User GetUserByUsername(string username)
    {
        return _bankRepository.GetAllUsers().FirstOrDefault(u => u.Username == username);
    }

    public User GetUserByUserId(int userId)
    {
        User? user = _bankRepository.GetUserByUserId(userId);
        if (user == null)
        {
            throw new Exception();
        }
        return user;
    }

    public Account GetAccountByAccountId(int accountId)
    {
        Account? account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            throw new Exception();
        }
        return account;
    }

    public List<Account> GetAccountsByUserId(int userId)
    {
        return _bankRepository.GetAccountsByUserId(userId);
    }

    public List<Transaction> GetTransactionsByUserId(int userId)
    {
        var accounts = _bankRepository.GetAccountsByUserId(userId);
        var transactions = new List<Transaction>();
        foreach (var account in accounts)
        {
            transactions.AddRange(_bankRepository.GetTransactionsByFromAccountId(account.AccountId));
            transactions.AddRange(_bankRepository.GetTransactionsByToAccountId(account.AccountId));
        }
        return transactions;
    }

    public List<Transaction> GetTransactionsByAccountId(int accountId)
    {
        List<Transaction> combinedTransactions = new List<Transaction>();
        List<Transaction> fromTransactions = _bankRepository.GetTransactionsByFromAccountId(accountId);
        List<Transaction> toTransactions = _bankRepository.GetTransactionsByToAccountId(accountId);
        combinedTransactions.AddRange(fromTransactions);
        combinedTransactions.AddRange(toTransactions);
        return combinedTransactions;
    }

    public string Withdraw(int accountId, double amount)
    {
        var account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            return "Account not found.";
        }

        if (amount <= 0)
        {
            return "Invalid amount.";
        }

        if (account.Balance < amount)
        {
            return "Insufficient funds.";
        }

        account.Balance -= amount;
        _bankRepository.CreateTransaction(new Transaction { FromAccount = account, Amount = amount });
        return "Withdrawal successful.";
    }

    public string Deposit(int accountId, double amount)
    {
        var account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            return "Account not found.";
        }

        if (amount <= 0)
        {
            return "Invalid amount.";
        }

        account.Balance += amount;
        _bankRepository.CreateTransaction(new Transaction { ToAccount = account, Amount = amount });
        return "Deposit successful.";
    }

    public string AddAccountToFamily(int userId, int accountId)
    {
        var user = _bankRepository.GetUserByUserId(userId);
        var account = _bankRepository.GetAccountByAccountId(accountId);

        if (user == null || account == null)
        {
            return "User or Account not found.";
        }

        if (account.Users.Contains(user))
        {
            return "User is already associated with this account.";
        }

        account.Users.Add(user);
        _bankRepository.CreateAccount(account);
        return "Account successfully added to family.";
    }

    public string RemoveAccountFromFamily(int userId, int accountId)
    {
        var user = _bankRepository.GetUserByUserId(userId);
        var account = _bankRepository.GetAccountByAccountId(accountId);

        if (user == null || account == null)
        {
            return "User or Account not found.";
        }

        if (!account.Users.Contains(user))
        {
            return "User is not associated with this account.";
        }

        account.Users.Remove(user);
        _bankRepository.CreateAccount(account);
        return "Account successfully removed from family.";
    }

    public string UpdateUserProfile(int userId, string newUsername, string newPassword)
    {
        var user = _bankRepository.GetUserByUserId(userId);
        if (user == null)
        {
            return "User not found.";
        }

        user.Username = newUsername;
        user.Password = newPassword;
        _bankRepository.CreateUser(user);
        return "Profile updated successfully.";
    }

    public Account CreateAccount(Account account)
    {
        account.AccountId = 0;
        return _bankRepository.CreateAccount(account);
    }

    public User CreateUser(User user)
    {
        user.UserId = 0;
        return _bankRepository.CreateUser(user);
    }
}