using System.Data;
using BankBackend.Models;
using BankBackend.Repository;
using Microsoft.EntityFrameworkCore;

public class BankRepositoryTests
{
    private readonly BankRepository _repository;
    private readonly BankContext _context;

    /// <summary>
    /// creates an in-memory database for repository tests
    /// </summary>
    public BankRepositoryTests()
    {
        _context = new BankContext(new DbContextOptionsBuilder<BankContext>()
                                                .UseInMemoryDatabase("BankRepositoryTests")
                                                .Options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _repository = new BankRepository(_context);
    }

    private void RemoveAllUsers()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
    }

    private void RemoveAllAccounts()
    {
        _context.Accounts.RemoveRange(_context.Accounts);
    }

    private void RemoveAllTransactions()
    {
        _context.Transactions.RemoveRange(_context.Transactions);
    }

    [Fact]
    public void GetUserByUserIdWhenNoUserExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //retrieve non-exist user
        User? user = _repository.GetUserByUserId(42);
        Assert.Null(user);
    }

    [Fact]
    public void GetUserByUserIdWhenUserExistsReturnsUser()
    {
        //create a new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        //upload user to database
        user = _repository.CreateUser(user);
        //retrieve user
        User? foundUser = _repository.GetUserByUserId(user.UserId);
        Assert.NotNull(foundUser);
        Assert.Equal(user.UserId, foundUser.UserId);
        Assert.Equal(user.Password, foundUser.Password);
        Assert.Equal(user.Name, foundUser.Name);
    }

    [Fact]
    public void GetUserByUsernameWhenNoUserExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //retrieve non-exist user
        User? user = _repository.GetUserByUsername("ASkywalker");
        Assert.Null(user);
    }

    [Fact]
    public void GetUserByUsernameWhenUserExistsReturnsUser()
    {
        //create a new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        //upload user to database
        user = _repository.CreateUser(user);
        //retrieve user
        User? foundUser = _repository.GetUserByUsername(user.Username);
        Assert.NotNull(foundUser);
        Assert.Equal(user.UserId, foundUser.UserId);
        Assert.Equal(user.Password, foundUser.Password);
        Assert.Equal(user.Name, foundUser.Name);
    }

    [Fact]
    public void GetAllUsersWhenNoUserExistsReturnsEmptyList()
    {
        //clear database
        RemoveAllUsers();
        //retireve all users but no users exist
        List<User> users = _repository.GetAllUsers();
        Assert.Empty(users);
    }

    [Fact]
    public void GetAllUsersWhenUserExistsReturnsList()
    {
        //clear database
        RemoveAllUsers();
        //create 3 new users
        User user1 = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        User user2 = new User("ilovedemocracy", "Emperor Palpatine", "EPalpatine");
        User user3 = new User("badfeeling", "Obi Wan Kenobi", "OWKenobi");
        //upload 3 users
        user1 = _repository.CreateUser(user1);
        user2 = _repository.CreateUser(user2);
        user3 = _repository.CreateUser(user3);

        //retrieve all users
        List<User> users = _repository.GetAllUsers();
        Assert.Contains(user1, users);
        Assert.Contains(user2, users);
        Assert.Contains(user3, users);
    }

    [Fact]
    public void GetAccountsByUserIdUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //retrieve accounts of non-exist user
        List<Account>? accounts = _repository.GetAccountsByUserId(42);
        Assert.Null(accounts);
    }

    [Fact]
    public void GetAccountsByUserIdUserHasNoAccountsReturnsEmptyList()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //retrieve accounts of the new user with no accounts
        List<Account>? accounts = _repository.GetAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Empty(accounts);
    }

    [Fact]
    public void GetAccountsByUserIdUserHasAccountsReturnsList()
    {
        //create uew user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //create new accounts
        Account account1 = _repository.CreateAccount(new Account());
        Account account2 = _repository.CreateAccount(new Account());
        Account account3 = _repository.CreateAccount(new Account());
        //add accounts to user
        _repository.AddAccountToUser(account1.AccountId, user.UserId);
        _repository.AddAccountToUser(account2.AccountId, user.UserId);
        _repository.AddAccountToUser(account3.AccountId, user.UserId);
        //retrieve accounts of user
        List<Account>? accounts = _repository.GetAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Equal(3, accounts.Count());
    }

    [Fact]
    public void CreateUserUpdatesUserId()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        User createdUser = _repository.CreateUser(user);
        Assert.NotNull(createdUser);
        Assert.Equal(user.Name, createdUser.Name);
        Assert.Equal(user.Password, createdUser.Password);
        //check if new UserId has been assigned
        Assert.NotEqual(0, createdUser.UserId);
    }

    [Fact]
    public void UpdatePasswordUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //update non-exist user
        User? updatedUser = _repository.UpdatePassword(42, "Ilovesand");
        Assert.Null(updatedUser);
    }

    [Fact]
    public void UpdatePasswordUserExistsUpdatesdUser()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //udpate user
        User? updatedUser = _repository.UpdatePassword(user.UserId, "ilovesand");
        Assert.NotNull(updatedUser);
        Assert.Equal("ilovesand", updatedUser.Password);
    }

    [Fact]
    public void UpdateUsernameUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //update non-exist user
        User? updatedUser = _repository.UpdateUsername(42, "Padme");
        Assert.Null(updatedUser);
    }

    [Fact]
    public void UpdateUsernameUserExistsUpdatesdUser()
    {
        //clear all users
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //update user
        User? updatedUser = _repository.UpdateUsername(user.UserId, "Padme");
        Assert.NotNull(updatedUser);
        Assert.Equal("Padme", updatedUser.Username);
    }

    [Fact]
    public void AddAccountToUserUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //create new account
        Account account = _repository.CreateAccount(new Account());
        //update non-exist user
        User? user = _repository.AddAccountToUser(account.AccountId, 42);
        Assert.Null(user);
    }

    [Fact]
    public void AddAccountToUserUserExistsUpdatesUserAccounts()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //add new account to user
        Account account1 = _repository.CreateAccount(new Account());
        User? updatedUser = _repository.AddAccountToUser(account1.AccountId, user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Single(updatedUser.Accounts);
        //add new account to user
        Account account2 = _repository.CreateAccount(new Account());
        updatedUser = _repository.AddAccountToUser(account2.AccountId, user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Equal(2, updatedUser.Accounts.Count());
        //add new account to user
        Account account3 = _repository.CreateAccount(new Account());
        updatedUser = _repository.AddAccountToUser(account3.AccountId, user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Equal(3, updatedUser.Accounts.Count());
    }

    [Fact]
    public void DeleteUserByIdUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //delete non-exist user
        User? user = _repository.DeleteUserById(42);
        Assert.Null(user);
    }

    [Fact]
    public void DeleteUserByIdUserExistsDeletesUser()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //delete that user
        User? deletedUser = _repository.DeleteUserById(user.UserId);
        Assert.NotNull(deletedUser);
        //try retrieving deleted user
        deletedUser = _repository.GetUserByUserId(deletedUser.UserId);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //delete account of non-exist user
        Account? account = _repository.DeleteUserAccountByAccountId(42, 42);
        Assert.Null(account);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserExistsButAccountDoesNotExistReturnsNull()
    {
        //create user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //delete non-exist account of existing user
        Account? deletedAccount = _repository.DeleteUserAccountByAccountId(user.UserId, 42);
        Assert.Null(deletedAccount);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserExistsAccountExistsDeletesAccount()
    {
        //create user
        User user = new User("ihatesand", "Anakin Skywalker", "ASkywalker");
        user = _repository.CreateUser(user);
        //create accoubt
        Account account = _repository.CreateAccount(new Account()); ;
        //add account to user
        User? updatedUser = _repository.AddAccountToUser(account.AccountId, user.UserId);
        Assert.NotNull(updatedUser);
        //delete that account
        Account? deletedAccount = _repository.DeleteUserAccountByAccountId(updatedUser.UserId, account.AccountId);
        Assert.NotNull(deletedAccount);
        //try retrieving deleted account  
        updatedUser = _repository.GetUserByUserId(updatedUser.UserId);
        Assert.NotNull(updatedUser);
        Assert.DoesNotContain(deletedAccount, updatedUser.Accounts);
    }

    [Fact]
    public void GetAccountByAccountIdWhenNoAccountExistsReturnsNull()
    {
        //retrieve non-exist account
        Account? account = _repository.GetAccountByAccountId(42);
        Assert.Null(account);
    }


    [Fact]
    public void GetAccountByAccountIdWhenAccountExistsReturnsAccount()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //retrieve created account
        Account? foundAccount = _repository.GetAccountByAccountId(account.AccountId);
        Assert.NotNull(foundAccount);
        Assert.Equal(account.Balance, foundAccount.Balance);
        Assert.Equal(account.Type, foundAccount.Type);
        Assert.Equal(account.PrimaryUserId, foundAccount.PrimaryUserId);
    }

    [Fact]
    public void GetAllAccountsNoAccountExistsReturnsEmptyList()
    {
        //clear all accounts
        RemoveAllAccounts();
        List<Account> accounts = _repository.GetAllAccounts();
        Assert.Empty(accounts);
    }

    [Fact]
    public void GetAllAccountsAccountExistReturnsList()
    {
        //clear all accounts
        RemoveAllAccounts();
        //create new accounts
        Account account1 = new Account();
        Account account2 = new Account();
        Account account3 = new Account();
        //upload accounts to database
        account1 = _repository.CreateAccount(account1);
        account2 = _repository.CreateAccount(account2);
        account3 = _repository.CreateAccount(account3);
        //retrieve all accounts
        List<Account> accounts = _repository.GetAllAccounts();
        Assert.Equal(3, accounts.Count());
    }

    [Fact]
    public void GetUsersByAccountIdAccountDoesNotExistReturnsNull()
    {
        RemoveAllAccounts();
        Account? account = _repository.GetAccountByAccountId(42);
        Assert.Null(account);
    }

    [Fact]
    public void GetUsersByAccountIdAccountHasNoUserReturnsEmptyList()
    {
        Account account = new Account();
        account = _repository.CreateAccount(account);
        List<User>? users = _repository.GetUsersByAccountId(account.AccountId);
        Assert.NotNull(users);
        Assert.Empty(users);
    }

    [Fact]
    public void GetUsersByAccountIdAccountHasUserReturnsList()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //create users for account
        User user1 = _repository.CreateUser(new User());
        User user2 = _repository.CreateUser(new User());
        User user3 = _repository.CreateUser(new User());
        _repository.AddUserToAccount(user1.UserId, account.AccountId);
        _repository.AddUserToAccount(user2.UserId, account.AccountId);
        _repository.AddUserToAccount(user3.UserId, account.AccountId);
        //retrieve users of account
        List<User>? users = _repository.GetUsersByAccountId(account.AccountId);
        Assert.NotNull(users);
        Assert.Equal(3, users.Count());
    }

    [Fact]
    public void GetPrimaryAccountsByUserIdUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //retrieve primary accounts
        List<Account>? accounts = _repository.GetPrimaryAccountsByUserId(42);
        Assert.Null(accounts);
    }

    [Fact]
    public void GetPrimaryAccountsByUserIdUserHasNoPrimaryAccountsReturnsEmptyList()
    {
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //retrieve primary accounts of user
        List<Account>? accounts = _repository.GetPrimaryAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Empty(accounts);
    }

    [Fact]
    public void GetPrimaryAccountsByUserIdUserHasPrimaryAccountsReturnsList()
    {
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //create new accounts and add user as primary user
        Account account1 = new Account();
        account1.PrimaryUserId = user.UserId;
        account1.Users.Add(user);
        account1 = _repository.CreateAccount(account1);
        Account account2 = new Account();
        account2.PrimaryUserId = user.UserId;
        account2.Users.Add(user);
        account2 = _repository.CreateAccount(account2);
        //retrieve the primary accounts
        List<Account>? accounts = _repository.GetPrimaryAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Equal(2, accounts.Count());
    }

    [Fact]
    public void CreateAccountUpdatesAccountId()
    {
        //create account
        Account account = new Account();
        Account createdAccount = _repository.CreateAccount(account);
        Assert.NotNull(createdAccount);
        Assert.Equal(account.Balance, createdAccount.Balance);
        Assert.Equal(account.Type, createdAccount.Type);
        Assert.Equal(account.PrimaryUserId, createdAccount.PrimaryUserId);
        //check if new AccountId has been assigned
        Assert.NotEqual(0, createdAccount.AccountId);
    }

    [Fact]
    public void UpdateBalanceAccountDoesNotExistReturnsNull()
    {
        //clear all accounts
        RemoveAllAccounts();
        //update non-exist account
        Account? account = _repository.UpdateBalance(42, 1000);
        Assert.Null(account);
    }

    [Fact]
    public void UpdateBalanceAccountExistsUpdatesAccount()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //update account
        Account? updatedAccount = _repository.UpdateBalance(account.AccountId, 2000);
        Assert.NotNull(updatedAccount);
        Assert.Equal(2000, account.Balance);
    }

    [Fact]
    public void UpdatePrimaryUserAccountDoesNotExistReturnsNull()
    {
        //clear all accounts
        RemoveAllAccounts();
        //create a user
        User user = new User();
        user = _repository.CreateUser(user);
        //update a non-exist account
        Account? account = _repository.UpdatePrimaryUser(42, user.UserId);
        Assert.Null(account);
    }

    [Fact]
    public void UpdatePrimaryUserUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //update account with non-exist user
        Account? updatedAccount = _repository.UpdatePrimaryUser(account.AccountId, 42);
        Assert.Null(updatedAccount);
    }

    [Fact]
    public void UpdatePrimaryUserUserAndAccountExistReturnsAccount()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //update primary user of account to user
        Account? updatedAccount = _repository.UpdatePrimaryUser(account.AccountId, user.UserId);
        Assert.NotNull(updatedAccount);
        Assert.Equal(user.UserId, updatedAccount.PrimaryUserId);
    }

    [Fact]
    public void AddUserToAccountAccountDoesNotExistReturnsNull()
    {
        //clears all accounts
        RemoveAllAccounts();
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //add that user to non-exist account
        Account? account = _repository.AddUserToAccount(user.UserId, 42);
        Assert.Null(account);
    }

    [Fact]
    public void AddUserToAccountAccountExistsUpdatesAccount()
    {
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //add that user to account
        Account? updatedAccount = _repository.AddUserToAccount(user.UserId, account.AccountId);
        Assert.NotNull(updatedAccount);
        Assert.Contains(user, updatedAccount.Users);

    }
    [Fact]

    public void DeleteAccountByIdAccountDoesNotExistReturnsNull()
    {
        //clear all accounts
        RemoveAllAccounts();
        //delete non-exist account
        Account? account = _repository.DeleteAccountById(42);
        Assert.Null(account);
    }

    [Fact]
    public void DeleteAccountByIdAccountExistsDeletesAccount()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //delete account
        Account? deletedAccount = _repository.DeleteAccountById(account.AccountId);
        Assert.NotNull(account);
        //try retrieving deleted account
        deletedAccount = _repository.GetAccountByAccountId(account.AccountId);
        Assert.Null(deletedAccount);
    }

    [Fact]
    public void DeleteAccountUserByUserIdAccountDoesNotExistReturnsNull()
    {
        //clear all accounts
        RemoveAllAccounts();
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //delete from non-exist account
        User? deletedUser = _repository.DeleteAccountUserByUserId(42, user.UserId);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void DeleteAccountUserByUserIdUserDoesNotExistReturnsNull()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //delete from non-exist user
        User? deletedUser = _repository.DeleteAccountUserByUserId(account.AccountId, 42);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void DeleteAccountUserByUserIdUserAndAccountExistReturnsDeletedUser()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //create new user
        User user = new User();
        user = _repository.CreateUser(user);
        //add user to account
        Account? updateAccount = _repository.AddUserToAccount(user.UserId, account.AccountId);
        Assert.NotNull(updateAccount);
        //delete the user from account
        User? deletedUser = _repository.DeleteAccountUserByUserId(updateAccount.AccountId, user.UserId);
        Assert.NotNull(deletedUser);
        Assert.DoesNotContain(deletedUser, updateAccount.Users);
    }

    [Fact]
    public void GetTransactionByTransactionIdTransactionDoesNotExistReturnsNull()
    {
        //clear all transactions
        RemoveAllTransactions();
        //retrieve non-exist transaction
        Transaction? transaction = _repository.GetTransactionByTransactionId(42);
        Assert.Null(transaction);
    }

    [Fact]
    public void GetTransactionByTransactionIdTransactionExistsReturnsTransaction()
    {
        //create new transaction
        Transaction transaction = new Transaction();
        transaction = _repository.CreateTransaction(transaction);
        //retrieve transaction
        Transaction? foundTransaction = _repository.GetTransactionByTransactionId(transaction.TransactionId);
        Assert.NotNull(foundTransaction);
    }

    [Fact]
    public void GetAllTransactionsNoTransactionsExistReturnsEmptyList()
    {
        //clears all transactions
        RemoveAllTransactions();
        //retrieve non-exist transactions
        List<Transaction> transactions = _repository.GetAllTransactions();
        Assert.Empty(transactions);
    }

    [Fact]
    public void GetAllTransactionsTransactionExistReturnsList()
    {
        //clears all transactions
        RemoveAllTransactions();
        //create transactions
        Transaction transaction1 = _repository.CreateTransaction(new Transaction());
        Transaction transaction2 = _repository.CreateTransaction(new Transaction());
        Transaction transaction3 = _repository.CreateTransaction(new Transaction());
        //retrieve all transactions
        List<Transaction> transactions = _repository.GetAllTransactions();
        Assert.Equal(3, transactions.Count());
    }

    [Fact]
    public void GetTransactionsByFromAccountFromAccountDoesNotExistReturnsEmptyList()
    {
        //clear all accounts
        RemoveAllAccounts();
        //retrieve transactions from non-exist from account
        List<Transaction> transactions = _repository.GetTransactionsByFromAccountId(42);
        Assert.Empty(transactions);
    }

    [Fact]
    public void GetTransactionsByFromAccountFromAccountExistsReturnsList()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //create new transaction
        Transaction transaction = new Transaction();
        transaction.FromAccount = account;
        transaction = _repository.CreateTransaction(transaction);
        //retrieve transactions from account
        List<Transaction> transactions = _repository.GetTransactionsByFromAccountId(account.AccountId);
        Assert.Contains(transaction, transactions);
    }

    [Fact]
    public void GetTransactionsByFromAccountToAccountDoesNotExistReturnsEmptyList()
    {
        //clear all accounts
        RemoveAllAccounts();
        //retrieve transactions from non-exist to account
        List<Transaction> transactions = _repository.GetTransactionsByToAccountId(42);
        Assert.Empty(transactions);

    }

    [Fact]
    public void GetTransactionsByFromAccountToAccountExistsReturnsList()
    {
        //create new account
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //create new transaction
        Transaction transaction = new Transaction();
        transaction.ToAccount = account;
        transaction = _repository.CreateTransaction(transaction);
        //retrieve transactions to account
        List<Transaction> transactions = _repository.GetTransactionsByToAccountId(account.AccountId);
        Assert.Contains(transaction, transactions);
    }

    [Fact]
    public void CreateTransactionUpdatesTransactionId()
    {
        Transaction transaction = new Transaction();
        transaction = _repository.CreateTransaction(transaction);
        Assert.NotEqual(0, transaction.TransactionId);
    }

    [Fact]
    public void DeleteTransactionByTransactionIdTransactionDoesNotExistReturnsNull()
    {
        //clear all transactions
        RemoveAllTransactions();
        //delete non-exist transaction
        Transaction? transaction = _repository.DeleteTransactionByTransactionId(42);
        Assert.Null(transaction);
    }

    [Fact]
    public void DeleteTransactionByTransactionIdTransactionExistsDeletesTransaction()
    {
        //create new transaction
        Transaction transaction = new Transaction();
        transaction = _repository.CreateTransaction(transaction);
        //delete transaction
        Transaction? deletedTransaction = _repository.DeleteTransactionByTransactionId(transaction.TransactionId);
        Assert.NotNull(deletedTransaction);
        //try retireving deleted transaction
        deletedTransaction = _repository.DeleteTransactionByTransactionId(deletedTransaction.TransactionId);
        Assert.Null(deletedTransaction);
    }
}