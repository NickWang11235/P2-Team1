using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;
using BankBackend.Controllers;
using System.Collections.Generic;
using System.Net;

namespace BankBackend.Tests.Controllers
{
    public class ControllersTests
    {
        private readonly Mock<IBankService> _mockBankService;
        private readonly Mock<ILogger<AccountController>> _mockAccountLogger;
        private readonly Mock<ILogger<UsersController>> _mockUserLogger;

        public ControllersTests()
        {
            _mockBankService = new Mock<IBankService>();
            _mockAccountLogger = new Mock<ILogger<AccountController>>();
            _mockUserLogger = new Mock<ILogger<UsersController>>();
        }

        [Fact]
        public void PostAccount_ReturnsCreatedAccount()
        {
            // Arrange
            var account = new Account 
            { 
                AccountId = 1,  Balance = 1000.0, Type = AccountType.CHECKING, PrimaryUserId = 1
            };
            _mockBankService.Setup(service => service.CreateAccount(account)).Returns(account);

            var controller = new AccountController(new Mock<ILogger<AccountController>>().Object, _mockBankService.Object);

            // Act
            var result = controller.PostAccount(account);

            // Assert
            Assert.Equal(account, result);
        }

        [Fact]
        public void GetAllAccounts_ReturnsListOfAccounts()
        {
            // Arrange
            var accounts = new List<Account>
            {
                new Account 
                { 
                    AccountId = 1, Balance = 500.0, Type = AccountType.SAVINGS, PrimaryUserId = 1             
                },
                new Account 
                { 
                    AccountId = 2, Balance = 1200.0, Type = AccountType.CHECKING, PrimaryUserId = 2              
                }
            };
            _mockBankService.Setup(service => service.GetAllAccounts()).Returns(accounts);

            var controller = new AccountController(new Mock<ILogger<AccountController>>().Object, _mockBankService.Object);

            // Act
            var result = controller.GetAllAccounts();

            // Assert
            Assert.Equal(accounts, result);
        }

        [Fact]
        public void GetTransactionsByAccountId_ReturnsTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionId = 1, Amount = 100.00 },   
                new Transaction { TransactionId = 2, Amount = 50.00 }     
            };
            int accountId = 1;

            _mockBankService.Setup(service => service.GetTransactionsByAccountId(accountId)).Returns(transactions);

            var controller = new AccountController(new Mock<ILogger<AccountController>>().Object, _mockBankService.Object);

            // Act
            var result = controller.GetTransactionsByAccountId(accountId);

            // Assert
            Assert.Equal(transactions, result);
        }

        // [Fact]
        // public void GetTransactionsByAccountId_ReturnsNotFoundWhenUserIdNotFound()
        // {
        //     // Arrange
        //     int accountId = 1;
        //     _mockBankService.Setup(s => s.GetTransactionsByAccountId(accountId)).Throws(new UserIdNotFoundException());
        //     var controller = new AccountController(_mockAccountLogger.Object, _mockBankService.Object);

        //     // Act
        //     var result = controller.GetTransactionsByAccountId(accountId);

        //     // Assert
        //     Assert.Null(result);
        //     Assert.Equal((int)HttpStatusCode.NotFound, controller.Response.StatusCode);
        // }

        [Fact]
        public void PostUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User
            {
                UserId = 1, Username = "ASkywalker", Password = "ihatesand", Name = "Askywalker", Accounts = new List<Account>() 
            };
            _mockBankService.Setup(service => service.CreateUser(user)).Returns(user);

            var controller = new UsersController(new Mock<ILogger<UsersController>>().Object, _mockBankService.Object);

            // Act
            var result = controller.PostUser(user);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Initialize properties
            var users = new List<User>
            {
                new User 
                { 
                    UserId = 1, Username = "ASkywalker", Password = "ihatesand", Name = "Askywalker", Accounts = new List<Account>() 
                },
                new User 
                { 
                    UserId = 2, Username = "EPalpatine", Password = "ilovedemocracy", Name = "Emperor Palpatine", Accounts = new List<Account>() 
                }
            };
            _mockBankService.Setup(service => service.GetAllUsers()).Returns(users);

            var controller = new UsersController(new Mock<ILogger<UsersController>>().Object, _mockBankService.Object);

            // Act
            var result = controller.GetAllUsers();

            // Assert
            Assert.Equal(users, result);
        }

        [Fact]
        public void Login_ReturnsUserOnSuccess()
        {
            // Arrange
            var user = new User { Username = "ASkywalker", Password = "ihatesand" };
            _mockBankService.Setup(s => s.ValidateLogin(user.Username, user.Password)).Returns(user);
            var controller = new UsersController(_mockUserLogger.Object, _mockBankService.Object);

            // Act
            var result = controller.Login(user);

            // Assert
            Assert.Equal(user, result);
        }

        // [Fact]
        // public void Login_ReturnsUnauthorizedWhenUsernameNotFound()
        // {
        //     // Arrange
        //     var user = new User { Username = "DVader", Password = "ihatesand" };
        //     _mockBankService.Setup(s => s.ValidateLogin(user.Username, user.Password)).Throws(new UsernameNotFoundException());
        //     var controller = new UsersController(_mockUserLogger.Object, _mockBankService.Object);

        //     // Act
        //     var result = controller.Login(user);

        //     // Assert
        //     Assert.Null(result);
        //     Assert.Equal((int)HttpStatusCode.Unauthorized, controller.Response.StatusCode);
        // }

        // [Fact]
        // public void Login_ReturnsUnauthorizedWhenPasswordIsInvalid()
        // {
        //     // Arrange
        //     var user = new User { Username = "ASkywalker", Password = "ilovesand" };
        //     _mockBankService.Setup(s => s.ValidateLogin(user.Username, user.Password)).Throws(new InvalidPasswordException());
        //     var controller = new UsersController(_mockUserLogger.Object, _mockBankService.Object);

        //     // Act
        //     var result = controller.Login(user);

        //     // Assert
        //     Assert.Null(result);
        //     Assert.Equal((int)HttpStatusCode.Unauthorized, controller.Response.StatusCode);
        // }
    }
}
