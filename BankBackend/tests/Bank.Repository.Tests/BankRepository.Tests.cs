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
        User user = new User("ihatesand", "Anakin Skywalker");
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
        User user1 = new User("ihatesand", "Anakin Skywalker");
        User user2 = new User("ilovedemocracy", "Emperor Palpatine");
        User user3 = new User("badfeeling", "Obi Wan Kenobi");
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
        User user = new User("ihatesand", "Anakin Skywalker");
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
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        //add accounts to user
        _repository.AddAccountToUser(new Account(), user.UserId);
        _repository.AddAccountToUser(new Account(), user.UserId);
        _repository.AddAccountToUser(new Account(), user.UserId);
        //retrieve accounts of user
        List<Account>? accounts = _repository.GetAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Equal(3, accounts.Count());
    }

    [Fact]
    public void CreateUserUpdatesUserId()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker");
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
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        //udpate user
        User? updatedUser = _repository.UpdatePassword(user.UserId, "ilovesand");
        Assert.NotNull(updatedUser);
        Assert.Equal("ilovesand", updatedUser.Password);
    }

    [Fact]
    public void UpdateNameUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //update non-exist user
        User? updatedUser = _repository.UpdateName(42, "Padme");
        Assert.Null(updatedUser);
    }

    [Fact]
    public void UpdateNameUserExistsUpdatesdUser()
    {
        //clear all users
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        //update user
        User? updatedUser = _repository.UpdateName(user.UserId, "Padme");
        Assert.NotNull(updatedUser);
        Assert.Equal("Padme", updatedUser.Name);
    }

    [Fact]
    public void AddAccountToUserUserDoesNotExistReturnsNull()
    {
        //clear all users
        RemoveAllUsers();
        //update non-exist user
        User? user = _repository.AddAccountToUser(new Account(), 42);
        Assert.Null(user);
    }

    [Fact]
    public void AddAccountToUserUserExistsUpdatesUserAccounts()
    {
        //create new user
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        //add new account to user
        User? updatedUser = _repository.AddAccountToUser(new Account(), user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Single(updatedUser.Accounts);
        //add new account to user
        updatedUser = _repository.AddAccountToUser(new Account(), user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Equal(2, updatedUser.Accounts.Count());
        //add new account to user
        updatedUser = _repository.AddAccountToUser(new Account(), user.UserId);
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
        User user = new User("ihatesand", "Anakin Skywalker");
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
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        //delete non-exist account of existing user
        Account? deletedAccount = _repository.DeleteUserAccountByAccountId(user.UserId, 42);
        Assert.Null(deletedAccount);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserExistsAccountExistsDeletesAccount()
    {
        //create user
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        //create accoubt
        Account account = new Account();
        account = _repository.CreateAccount(account);
        //add account to user
        User? updatedUser = _repository.AddAccountToUser(account, user.UserId);
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
        _repository.AddUserToAccount(new User(), account.AccountId);
        _repository.AddUserToAccount(new User(), account.AccountId);
        _repository.AddUserToAccount(new User(), account.AccountId);
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
        Account account =  new Account();
        account = _repository.CreateAccount(account);
        //update account
        Account? updatedAccount = _repository.UpdateBalance(account.AccountId, 2000);
        Assert.NotNull(updatedAccount);
        Assert.Equal(2000, account.Balance);
    }

    [Fact]
    public void UpdatePrimaryUserAccountDoesNotExistReturnsNull()
    {
        RemoveAllAccounts();
        Account? account = _repository.UpdatePrimaryUser(42, 42);
    }

    [Fact]
    public void UpdatePrimaryUserUserDoesNotExistReturnsNull()
    {

    }

    [Fact]
    public void UpdatePrimaryUserUserAndAccountExistReturnsAccount()
    {

    }

}