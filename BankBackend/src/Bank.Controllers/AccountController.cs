namespace BankBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;
using System.Net;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    private readonly IBankService _bankService;

    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IBankService bankService)
    {
        _bankService = bankService;
        _logger = logger;
    }

    [HttpPost("")]
    public Account PostAccount([FromBody] Account account)
    {
        return _bankService.CreateAccount(account);
    }

    [HttpGet("")]
    public List<Account> GetAllAccounts()
    {
        return _bankService.GetAllAccounts();
    }

    [HttpGet("{id}")]
    public List<Transaction>? GetTransactionsByAccountId([FromRoute] int id)
    {
        try
        {
            return _bankService.GetTransactionsByAccountId(id);
        }
        catch (UserIdNotFoundException)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return null;
        }
    }

    // [HttpPatch("{accountId}/add")]
    // public Account AddUserToAccountById([FromRoute] int accountId, [FromBody] int userId)
    // {
    //     _bankService.AddAccountUser(accountId, userId);
    //     return _bankService.GetAccountByAccountId(accountId);
    // }

    // [HttpPatch("{accountId}/remove")]
    // public Account RemoveUserFromAccountById([FromRoute] int accountId, [FromBody] int userId)
    // {
    //     _bankService.RemoveAccountUser(accountId, userId);
    //     return _bankService.GetAccountByAccountId(accountId);
    // }

}
