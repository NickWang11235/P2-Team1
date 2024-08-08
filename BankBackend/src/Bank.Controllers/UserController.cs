namespace BankBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;

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

    [HttpPost("login")]
    public User Login([FromBody] User user)
    {
        return _bankService.ValidateLogin(user.Username,user.Password);
    }

    [HttpGet("{id}/accounts")]
    public List<Account> GetAccountsByUserId([FromRoute] int id)
    {
        return _bankService.GetAccountsByUserId(id);
    }

    [HttpPatch("{userId}")]
    public User UpdateUserInfo([FromRoute] int userId, [FromBody] User user)
    {
        // return _bankService.UpdateUserInfo(userId,)
        throw new NotImplementedException();
    }

    [HttpPatch("{userId}/add")]
    public Account AddUserToAccountById([FromRoute] int accountId, [FromBody] int newId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId}/remove")]
    public Account RemoveUserFromAccountById([FromRoute] int accountId, [FromBody] int newId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("logout")]
    public void Logout()
    {
        throw new NotImplementedException();
    }


}
