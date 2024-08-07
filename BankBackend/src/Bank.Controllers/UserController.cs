namespace BankBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly IBankService _gameService;

    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger, IBankService gameService)
    {
        _gameService = gameService;
        _logger = logger;
    }

    [HttpPost("")]
    public User PostUser(User user){
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public bool Login(User user)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/accounts")]
    public List<Account> GetAccountsByUserId(int id){
        throw new NotImplementedException();
    }

    [HttpPatch("{userId}")]
    public User UpdateUserInfo(User user){
        throw new NotImplementedException();
    }

    [HttpPatch("{userId}/userName")]
    public Account AddUserToAccountById(int accountId, int newId){
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId}/remove")]
    public Account RemoveUserFromAccountById(int accountId, int newId){
        throw new NotImplementedException();
    }

    [HttpPost("logout")]
    public bool Logout(User user)
    {
        throw new NotImplementedException();
    }


}
