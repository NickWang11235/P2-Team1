namespace BankBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;
using System.Net;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly IBankService _bankService;

    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger, IBankService bankService)
    {
        _bankService = bankService;
        _logger = logger;
    }

    [HttpPost("")]
    public User PostUser([FromBody] User user)
    {
        return _bankService.CreateUser(user);
    }

    [HttpGet("")]
    public List<User> GetAllUsers()
    {
        return _bankService.GetAllUsers();
    }

    [HttpGet("{userId}")]
    public User? GetUsersById([FromRoute] int userId)
    {
        try
        {
            return _bankService.GetUserByUserId(userId);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
    }

    [HttpPost("login")]
    public User? Login([FromBody] User user)
    {
        try
        {
            return _bankService.ValidateLogin(user.Username, user.Password);
        }
        catch (UsernameNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
        catch (InvalidPasswordException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
    }

    [HttpGet("{id}/accounts")]
    public List<Account>? GetAccountsByUserId([FromRoute] int id)
    {
        try
        {
            return _bankService.GetAccountsByUserId(id);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
    }

    [HttpGet("{id}/transactions")]
    public List<Transaction>? GetTransactionsByUserId([FromRoute] int id)
    {
        try
        {
            return _bankService.GetTransactionsByUserId(id);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
    }

    [HttpPatch("{userId}")]
    public User? UpdateUserInfo([FromRoute] int userId, [FromBody] User user)
    {
        try
        {
            return _bankService.UpdateUserProfile(userId, user.Username, user.Password);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (RepositoryException)
        {
            Response.StatusCode = 500;
            return null;
        }
    }
    
    [HttpPatch("{userId}/add/{addedAccount}")]
    public Account? AddAccountToUserById([FromRoute] int userId, [FromRoute] int addedAccount, [FromBody] int accountId)
    {
        try
        {
            _bankService.AddAccountUser(userId, addedAccount, accountId);
            return _bankService.GetAccountByAccountId(accountId);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (AccountIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (UserNotAuthorizedException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
    }

    [HttpPatch("{userId}/remove")]
    public Account? RemoveUserFromAccountById([FromRoute] int userId, [FromBody] int accountId)
    {
        try
        {
            _bankService.RemoveAccountUser(userId, accountId);
            return _bankService.GetAccountByAccountId(accountId);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (AccountIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (UserNotAuthorizedException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
    }

    [HttpPatch("{userId}/deposit")]
    public Transaction? Deposit([FromRoute] int userId, [FromQuery] int accoubtId, [FromQuery] double amount)
    {
        try
        {
            return _bankService.Deposit(userId, accoubtId, amount);
        }
        catch (AccountIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (UserNotAuthorizedException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
        catch (RepositoryException)
        {
            Response.StatusCode = 500;
            return null;
        }
    }

    [HttpPatch("{userId}/withdraw")]
    public Transaction? Withdraw([FromRoute] int userId, [FromQuery] int accoubtId, [FromQuery] double amount)
    {
        try
        {
            return _bankService.Withdraw(userId, accoubtId, amount);
        }
        catch (AccountIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (UserNotAuthorizedException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
        catch (InsufficientFundsException)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return null;
        }
        catch (RepositoryException)
        {
            Response.StatusCode = 500;
            return null;
        }
    }


    [HttpPatch("{userId}/transfer")]
    public Transaction? Transfer([FromRoute] int userId, [FromQuery] int fromAccountId, [FromQuery] int toAccountId, double amount)
    {
        try
        {
            return _bankService.Transfer(userId, fromAccountId, toAccountId, amount);
        }
        catch (AccountIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
        catch (UserNotAuthorizedException)
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return null;
        }
        catch (InsufficientFundsException)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return null;
        }
    }

    // [HttpPost("logout")]
    // public void Logout()
    // {
    //     throw new NotImplementedException();
    // }


}
