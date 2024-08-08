namespace BankBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankBackend.Service;
using BankBackend.Models;

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

    [HttpGet("{id}")]
    public List<Transaction> GetTransactionsByAccountId([FromRoute] int id)
    {
        return _bankService.GetTransactionsByAccountId(id);
    }

    // [HttpPatch("{id}")]
    // public Account PatchAccountById([FromRoute] int id, [FromBody] Account account)
    // {

    // }

    [HttpPatch("{accountId}/add")]
    public Account AddUserToAccountById([FromRoute] int accountId, [FromBody] int userId)
    {
        _bankService.AddAccountToFamily(accountId, userId);
        return _bankService.GetAccountByAccountId(accountId);
    }

    [HttpPatch("{accountId}/remove")]
    public Account RemoveUserFromAccountById([FromRoute] int accountId, [FromBody] int userId)
    {
        _bankService.RemoveAccountFromFamily(accountId, userId);
        return _bankService.GetAccountByAccountId(accountId);
    }

}
