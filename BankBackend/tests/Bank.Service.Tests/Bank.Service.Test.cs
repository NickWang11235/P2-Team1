using Xunit;
using Moq;
using BankBackend.Repository;
using BankBackend.Models;
using BankBackend.Service;

public class BankServiceTests
{
    private readonly Mock<IBankRepository> _mockRepository;
    private readonly BankService _bankService;

    public BankServiceTests()
    {
        _mockRepository = new Mock<IBankRepository>();
        _bankService = new BankService(_mockRepository.Object);
    }

    [Fact]
    public void ValidateLogin_ValidCredentials_ReturnsUser()
    {
        // Arrange
        var username = "ASkywalker";
        var password = "ihatesand";
        var user = new User { Username = username, Password = password };
        _mockRepository.Setup(r => r.GetUserByUsername(username)).Returns(user);

        // Act
        var result = _bankService.ValidateLogin(username, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
    }

    [Fact]
    public void ValidateLogin_InvalidUsername_ThrowsUsernameNotFoundException()
    {
        // Arrange
        var username = "DVader";
        var password = "ihatesand";
        _mockRepository.Setup(r => r.GetUserByUsername(username)).Returns((User?)null);

        // Act & Assert
        Assert.Throws<UsernameNotFoundException>(() => _bankService.ValidateLogin(username, password));
    }

    [Fact]
    public void ValidateLogin_InvalidPassword_ThrowsInvalidPasswordException()
    {
        // Arrange
        var username = "ASkywalker";
        var password = "ilovesand";
        var user = new User { Username = username, Password = "ihatesand" };
        _mockRepository.Setup(r => r.GetUserByUsername(username)).Returns(user);

        // Act & Assert
        Assert.Throws<InvalidPasswordException>(() => _bankService.ValidateLogin(username, password));
    }

    [Fact]
    public void GetUserByUserId_ValidId_ReturnsUser()
    {
        // Arrange
        var userId = 1;
        var user = new User { UserId = userId };
        _mockRepository.Setup(r => r.GetUserByUserId(userId)).Returns(user);

        // Act
        var result = _bankService.GetUserByUserId(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
    }

    [Fact]
    public void GetUserByUserId_InvalidId_ThrowsUserIdNotFoundException()
    {
        // Arrange
        var userId = 1;
        _mockRepository.Setup(r => r.GetUserByUserId(userId)).Returns((User?)null);

        // Act & Assert
        Assert.Throws<UserIdNotFoundException>(() => _bankService.GetUserByUserId(userId));
    }

    [Fact]
    public void Deposit_ValidAccount_IncreasesBalance()
    {
        // Arrange
        var accountId = 1;
        var amount = 100.0;
        var initialBalance = 200.0;
        var updatedBalance = initialBalance + amount;

        var userId = 1;
        List<User> users = new List<User>();
        users.Add(new User { UserId = userId });

        var account = new Account { AccountId = accountId, Balance = initialBalance, Users = users };
        var updatedAccount = new Account { AccountId = accountId, Balance = updatedBalance };

        _mockRepository.Setup(r => r.GetAccountByAccountId(accountId)).Returns(account);
        _mockRepository.Setup(r => r.UpdateBalance(accountId, updatedBalance)).Returns(updatedAccount);
        _mockRepository.Setup(r => r.CreateTransaction(It.IsAny<Transaction>())).Returns(new Transaction { FromAccount = account, Amount = amount });

        // Act
        var result = _bankService.Deposit(userId, accountId, amount);

        // Assert
        _mockRepository.Verify(r => r.UpdateBalance(accountId, updatedBalance), Times.Once);
        _mockRepository.Verify(r => r.CreateTransaction(It.IsAny<Transaction>()), Times.Once);

        Assert.Equal(amount, result.Amount);
    }

    [Fact]
    public void Withdraw_InsufficientFunds_ThrowsInsufficientFundsException()
    {
        // Arrange
        var accountId = 1;
        var amount = 100.0;

        var userId = 1;
        List<User> users = new List<User>();
        users.Add(new User { UserId = userId });
        var account = new Account { AccountId = accountId, Balance = 50.0, Users = users };
        _mockRepository.Setup(r => r.GetAccountByAccountId(accountId)).Returns(account);

        // Act & Assert
        Assert.Throws<InsufficientFundsException>(() => _bankService.Withdraw(userId, accountId, amount));
    }

    [Fact]
    public void AddAccountUser_ValidData_AddsUserToAccount()
    {
        // Arrange
        var userId = 1;
        var accountId = 1;
        var user = new User { UserId = userId };
        var account = new Account { AccountId = accountId, PrimaryUserId = userId };
        _mockRepository.Setup(r => r.GetUserByUserId(userId)).Returns(user);
        _mockRepository.Setup(r => r.GetAccountByAccountId(accountId)).Returns(account);
        _mockRepository.Setup(r => r.AddUserToAccount(userId, accountId)).Verifiable();
        _mockRepository.Setup(r => r.AddAccountToUser(accountId, userId)).Verifiable();

        // Act
        var result = _bankService.AddAccountUser(userId, accountId);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify();
    }

    [Fact]
    public void RemoveAccountUser_ValidData_RemovesUserFromAccount()
    {
        // Arrange
        var userId = 1;
        var accountId = 1;
        var user = new User { UserId = userId };
        var account = new Account { AccountId = accountId, Users = new List<User> { user } , PrimaryUserId = userId};
        _mockRepository.Setup(r => r.GetUserByUserId(userId)).Returns(user);
        _mockRepository.Setup(r => r.GetAccountByAccountId(accountId)).Returns(account);
        _mockRepository.Setup(r => r.DeleteUserAccountByAccountId(userId, accountId)).Verifiable();
        _mockRepository.Setup(r => r.DeleteAccountUserByUserId(accountId, userId)).Verifiable();

        // Act
        var result = _bankService.RemoveAccountUser(userId, accountId);

        // Assert
        Assert.NotNull(result);
        _mockRepository.Verify();
    }
}
