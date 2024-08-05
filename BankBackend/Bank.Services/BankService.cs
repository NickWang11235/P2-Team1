using System;
using System.Collections.Generic;
using System.Linq;

namespace BankService;

public class BankService : IBankService
{
    private readonly IBankRepository _repository;

    public BankService(IBankRepository repository)
    {
        _repository = repository;
    }

    public string ValidateLogin(string username, string password)
    {
        var user = _repository.GetAllUsers().FirstOrDefault(u => u.Username == username);
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
        return _repository.GetAllUsers().FirstOrDefault(u => u.Username == username);
    }

    public List<Account> GetUserAccounts(int userId)
    {
        return _repository.GetAccountsByUserId(userId);
    }

    public List<Transaction> GetUserTransactions(int userId)
    {
        var accounts = _repository.GetAccountsByUserId(userId);
        var transactions = new List<Transaction>();
        foreach (var account in accounts)
        {
            transactions.AddRange(_repository.GetTransactionsByFromAccount(account.AccountId));
            transactions.AddRange(_repository.GetTransactionsByToAccountId(account.AccountId));
        }
        return transactions;
    }

    public string Withdraw(int accountId, double amount)
    {
        var account = _repository.GetAccountByAccountId(accountId);
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
        _repository.CreateTransaction(new Transaction { FromAccount = account, Amount = amount });
        return "Withdrawal successful.";
    }

    public string Deposit(int accountId, double amount)
    {
        var account = _repository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            return "Account not found.";
        }

        if (amount <= 0)
        {
            return "Invalid amount.";
        }

        account.Balance += amount;
        _repository.CreateTransaction(new Transaction { ToAccount = account, Amount = amount });
        return "Deposit successful.";
    }

    public string AddAccountToFamily(int userId, int accountId)
    {
        var user = _repository.GetUserByUserId(userId);
        var account = _repository.GetAccountByAccountId(accountId);

        if (user == null || account == null)
        {
            return "User or Account not found.";
        }

        if (account.Users.Contains(user))
        {
            return "User is already associated with this account.";
        }

        account.Users.Add(user);
        _repository.CreateAccount(account);
        return "Account successfully added to family.";
    }

    public string RemoveAccountFromFamily(int userId, int accountId)
    {
        var user = _repository.GetUserByUserId(userId);
        var account = _repository.GetAccountByAccountId(accountId);

        if (user == null || account == null)
        {
            return "User or Account not found.";
        }

        if (!account.Users.Contains(user))
        {
            return "User is not associated with this account.";
        }

        account.Users.Remove(user);
        _repository.CreateAccount(account);
        return "Account successfully removed from family.";
    }

    public string UpdateUserProfile(int userId, string newUsername, string newPassword)
    {
        var user = _repository.GetUserByUserId(userId);
        if (user == null)
        {
            return "User not found.";
        }

        user.Username = newUsername;
        user.Password = newPassword;
        _repository.CreateUser(user);
        return "Profile updated successfully.";
    }
}

