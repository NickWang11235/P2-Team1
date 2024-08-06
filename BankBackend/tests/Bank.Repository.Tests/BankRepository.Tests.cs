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

    [Fact]
    public void GetUserByUserIdWhenNoUserReturnsNull()
    {
        RemoveAllUsers();
        User? user = _repository.GetUserByUserId(42);
        Assert.Null(user);
    }

    [Fact]
    public void GetUserByUserIdWhenUserExistsReturnsUser()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        User? foundUser = _repository.GetUserByUserId(user.UserId);
        Assert.NotNull(foundUser);
        Assert.Equal(user.UserId, foundUser.UserId);
        Assert.Equal(user.Password, foundUser.Password);
        Assert.Equal(user.Name, foundUser.Name);
    }

    [Fact]
    public void GetAllUsersWhenNoUserExistsReturnsEmptyList()
    {
        RemoveAllUsers();
        List<User> users = _repository.GetAllUsers();
        Assert.Empty(users);
    }

    [Fact]
    public void GetAllUsersWhenUserExistsReturnsList()
    {
        RemoveAllUsers();
        User user1 = new User("ihatesand", "Anakin Skywalker");
        User user2 = new User("ilovedemocracy", "Emperor Palpatine");
        User user3 = new User("badfeeling", "Obi Wan Kenobi");
        user1 = _repository.CreateUser(user1);
        user2 = _repository.CreateUser(user2);
        user3 = _repository.CreateUser(user3);

        List<User> users = _repository.GetAllUsers();
        Assert.Contains(user1, users);
        Assert.Contains(user2, users);
        Assert.Contains(user3, users);
    }

    [Fact]
    public void GetAccountsByUserIdUserDoesNotExistReturnsNull()
    {
        RemoveAllUsers();
        List<Account>? accounts = _repository.GetAccountsByUserId(42);
        Assert.Null(accounts);
    }

    [Fact]
    public void GetAccountsByUserIdUserHasNoAccountsReturnsEmptyList()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        List<Account>? accounts = _repository.GetAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Empty(accounts);
    }

    [Fact]
    public void GetAccountsByUserIdUserHasAccountsReturnsList()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        _repository.AddAccountToUser(new Account(), user.UserId);
        _repository.AddAccountToUser(new Account(), user.UserId);
        _repository.AddAccountToUser(new Account(), user.UserId);
        List<Account>? accounts = _repository.GetAccountsByUserId(user.UserId);
        Assert.NotNull(accounts);
        Assert.Equal(3, accounts.Count());
    }

    [Fact]
    public void CreateUserUpdatesUserId()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        User createdUser = _repository.CreateUser(user);
        Assert.NotNull(createdUser);
        Assert.Equal(user.Name, createdUser.Name);
        Assert.Equal(user.Password, createdUser.Password);
        Assert.NotEqual(0, createdUser.UserId);
    }

    [Fact]
    public void UpdatePasswordUserDoesNotExistReturnsNull()
    {
        RemoveAllUsers();
        User? updatedUser = _repository.UpdatePassword(42, "Ilovesand");
        Assert.Null(updatedUser);
    }

    [Fact]
    public void UpdatePasswordUserExistsUpdatesdUser()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        User? updatedUser = _repository.UpdatePassword(user.UserId, "ilovesand");
        Assert.NotNull(updatedUser);
        Assert.Equal("ilovesand", updatedUser.Password);
    }

    [Fact]
    public void UpdateNameUserDoesNotExistReturnsNull()
    {
        RemoveAllUsers();
        User? updatedUser = _repository.UpdateName(42, "Padme");
        Assert.Null(updatedUser);
    }

    [Fact]
    public void UpdateNameUserExistsUpdatesdUser()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        User? updatedUser = _repository.UpdateName(user.UserId, "Padme");
        Assert.NotNull(updatedUser);
        Assert.Equal("Padme", updatedUser.Name);
    }

    [Fact]
    public void AddAccountToUserUserDoesNotExistReturnsNull()
    {
        RemoveAllUsers();
        User? user = _repository.AddAccountToUser(new Account(), 42);
        Assert.Null(user);
    }

    [Fact]
    public void AddAccountToUserUserExistsUpdatesUserAccounts()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        User? updatedUser = _repository.AddAccountToUser(new Account(), user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Single(updatedUser.Accounts);
        updatedUser = _repository.AddAccountToUser(new Account(), user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Equal(2, updatedUser.Accounts.Count());
        updatedUser = _repository.AddAccountToUser(new Account(), user.UserId);
        Assert.NotNull(updatedUser);
        Assert.Equal(3, updatedUser.Accounts.Count());
    }

    [Fact]
    public void DeleteUserByIdUserDoesNotExistReturnsNull()
    {
        RemoveAllUsers();
        User? user = _repository.DeleteUserById(42);
        Assert.Null(user);
    }

    [Fact]
    public void DeleteUserByIdUserExistsDeletesUser()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        User? deletedUser = _repository.DeleteUserById(user.UserId);
        Assert.NotNull(deletedUser);
        deletedUser = _repository.GetUserByUserId(deletedUser.UserId);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserDoesNotExistReturnsNull()
    {
        RemoveAllUsers();
        Account? account = _repository.DeleteUserAccountByAccountId(42, 42);
        Assert.Null(account);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserExistsButAccountDoesNotExistReturnsNull()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        Account? deletedAccount = _repository.DeleteUserAccountByAccountId(user.UserId, 42);
        Assert.Null(deletedAccount);
    }

    [Fact]
    public void DeleteUserAccountByAccountIdUserExistsAccountExistsDeletesAccount()
    {
        User user = new User("ihatesand", "Anakin Skywalker");
        user = _repository.CreateUser(user);
        Account account = new Account();
        account = _repository.CreateAccount(account);
        User? updatedUser = _repository.AddAccountToUser(account, user.UserId);
        Assert.NotNull(updatedUser);
        Account? deletedAccount = _repository.DeleteUserAccountByAccountId(updatedUser.UserId, account.AccountId);
        Assert.NotNull(deletedAccount);
        updatedUser = _repository.GetUserByUserId(updatedUser.UserId);
        Assert.NotNull(updatedUser);
        Assert.DoesNotContain(deletedAccount, updatedUser.Accounts);
    }

}