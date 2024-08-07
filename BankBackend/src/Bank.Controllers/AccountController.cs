namespace BankBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    private readonly IBankService _gameService;

    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IBankService gameService)
    {
        _gameService = gameService;
        _logger = logger;
    }

    [HttpPost("")]
    public Account PostAccount(Account account){
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public List<Transaction> GetTransactionsByAccountId(int id){
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    public Account PatchAccountById(int id){
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId}/add")]
    public Account AddUserToAccountById(int accountId, int newId){
        _gameService.AddAccountToFamily(accountId, newId);
        // _gameService.AddAccountToFamily(accountId, user.UserId);
        throw new NotImplementedException();
    }

    [HttpPatch("{accountId}/remove")]
    public Account RemoveUserFromAccountById(int accountId, int newId){
        throw new NotImplementedException();
    }

}
