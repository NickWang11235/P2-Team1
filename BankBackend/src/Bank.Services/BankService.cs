using BankBackend.Repository;
using BankBackend.Models;
using BankBackend.Service;

namespace BankBackend.Service;

public class BankService : IBankService
{
    private readonly IBankRepository _bankRepository;

    public BankService(IBankRepository repository)
    {
        _bankRepository = repository;
    }

    public User ValidateLogin(string username, string password)
    {
        User? user = _bankRepository.GetUserByUsername(username);
        if (user == null)
        {
            throw new UsernameNotFoundException("Username does not exist.");
        }

        if (user.Password != password)
        {
            throw new InvalidPasswordException("Invalid Password.");
        }

        return user;
    }

    public List<User> GetAllUsers(){
        return _bankRepository.GetAllUsers();
    }

    public User GetUserByUsername(string username)
    {
        User? user = _bankRepository.GetUserByUsername(username);
        if (user == null)
        {
            throw new UsernameNotFoundException("Username does not exist.");
        }
        return user;
    }

    public User GetUserByUserId(int userId)
    {
        User? user = _bankRepository.GetUserByUserId(userId);
        if (user == null)
        {
            throw new UserIdNotFoundException("User with userId does not exist.");
        }
        return user;
    }

    public Account GetAccountByAccountId(int accountId)
    {
        Account? account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            throw new AccountIdNotFoundException("Account with accountId does not exist.");
        }
        return account;
    }

    public List<Account> GetAccountsByUserId(int userId)
    {
        List<Account>? accounts = _bankRepository.GetAccountsByUserId(userId);
        if (accounts == null)
        {
            throw new UserIdNotFoundException("User with userId does not exist.");
        }
        return accounts;
    }

    public List<Transaction> GetTransactionsByUserId(int userId)
    {
        List<Account>? accounts = _bankRepository.GetAccountsByUserId(userId);
        List<Transaction> transactions = new List<Transaction>();

        if (accounts == null)
        {
            throw new UserIdNotFoundException("User with userId does not exist.");
        }

        foreach (var account in accounts)
        {
            transactions.AddRange(_bankRepository.GetTransactionsByFromAccountId(account.AccountId));
            transactions.AddRange(_bankRepository.GetTransactionsByToAccountId(account.AccountId));
        }
        return transactions;
    }

    public List<Transaction> GetTransactionsByAccountId(int accountId)
    {
        Account? account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            throw new AccountIdNotFoundException("Account with accountId does not exist.");
        }
        List<Transaction> combinedTransactions = new List<Transaction>();
        List<Transaction> fromTransactions = _bankRepository.GetTransactionsByFromAccountId(accountId);
        List<Transaction> toTransactions = _bankRepository.GetTransactionsByToAccountId(accountId);
        combinedTransactions.AddRange(fromTransactions);
        combinedTransactions.AddRange(toTransactions);
        return combinedTransactions;
    }

    public Transaction Deposit(int accountId, double amount)
    {
        Account? account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            throw new AccountIdNotFoundException("Account with accountId does not exist.");
        }
        account = _bankRepository.UpdateBalance(accountId, account.Balance + amount);
        if (account == null)
        {
            throw new RepositoryException("Unknown repository exception.");
        }
        return _bankRepository.CreateTransaction(new Transaction { FromAccount = account, Amount = amount });
    }

    public Transaction Withdraw(int accountId, double amount)
    {
        Account? account = _bankRepository.GetAccountByAccountId(accountId);
        if (account == null)
        {
            throw new AccountIdNotFoundException("Account with accountId does not exist.");
        }

        if (account.Balance < amount)
        {
            throw new InsufficientFundsException("Insufficient Funds.");
        }

        account = _bankRepository.UpdateBalance(accountId, account.Balance - amount);
        if (account == null)
        {
            throw new RepositoryException("Unknown repository exception.");
        }
        return _bankRepository.CreateTransaction(new Transaction { FromAccount = account, Amount = -amount });
    }

    public Transaction Transfer(int fromAccountId, int toAccountId, double amount){
        Account? fromAccount = _bankRepository.GetAccountByAccountId(fromAccountId);
        Account? toAccount = _bankRepository.GetAccountByAccountId(toAccountId);
        if (fromAccount == null ){
            throw new AccountIdNotFoundException("FromAccount does not exist.");
        }
        if(toAccount == null){
            throw new AccountIdNotFoundException("ToAccount does not exist.");
        }
        if(fromAccount.Balance < amount){
            throw new InsufficientFundsException("Insufficient Funds.");
        }
        _bankRepository.UpdateBalance(fromAccountId, fromAccount.Balance - amount);
        _bankRepository.UpdateBalance(toAccountId, toAccount.Balance + amount);
        return _bankRepository.CreateTransaction(new Transaction(fromAccount, toAccount, amount));
    }

    public User AddAccountUser(int userId, int accountId)
    {
        User? user = _bankRepository.GetUserByUserId(userId);
        Account? account = _bankRepository.GetAccountByAccountId(accountId);

        if (user == null)
        {
            throw new UserIdNotFoundException("User with userId does not exist.");
        }

        if (account == null)
        {
            throw new AccountIdNotFoundException("Account with accountId does not exist.");
        }

        if (account.Users.Contains(user))
        {
            return user;
        }


        _bankRepository.AddUserToAccount(userId, accountId);
        _bankRepository.AddAccountToUser(accountId, userId);
        return user;
    }

    public User RemoveAccountUser(int userId, int accountId)
    {
        User? user = _bankRepository.GetUserByUserId(userId);
        Account? account = _bankRepository.GetAccountByAccountId(accountId);

        if (user == null)
        {
            throw new UserIdNotFoundException("User with userId does not exist.");
        }

        if (account == null)
        {
            throw new AccountIdNotFoundException("Account with accountId does not exist.");
        }

        if (!account.Users.Contains(user))
        {
            return user;
        }

        _bankRepository.DeleteUserAccountByAccountId(userId, accountId);
        _bankRepository.DeleteAccountUserByUserId(accountId, userId);
        return user;
    }

    public User UpdateUserProfile(int userId, string newUsername, string newPassword)
    {
        User? user = _bankRepository.GetUserByUserId(userId);
        if (user == null)
        {
            throw new UserIdNotFoundException("User with userId does not exist.");
        }
        User? existingUser = _bankRepository.GetUserByUsername(newUsername);
        if (existingUser != null)
        {
            throw new UsernameAlreadyExistsException("Username is already in use.");
        }
        _bankRepository.UpdatePassword(userId, newPassword);
        user = _bankRepository.UpdateUsername(userId, newUsername);
        if (user == null)
        {
            throw new RepositoryException("Unknown repository exception.");
        }
        return user;
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