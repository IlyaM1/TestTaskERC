using AccountWebAPI.Database;
using AccountWebAPI.Database.Models;

using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace AccountWebAPI.Controllers;

[ApiController]
[Route("api/v1")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly AppDbContext _dbContext;

    public AccountController(ILogger<AccountController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Route("accounts")]
    [HttpGet]
    async public Task<IResult> GetAccounts()
    {
        var accounts = _dbContext.Accounts.ToList();

        return Results.Json(accounts);
    }

    [Route("accounts")]
    [HttpPost]
    async public Task<IResult> PostAccount(Account account)
    {
        Console.WriteLine("This is POST account!");
        if (!_ValidateNewAccount(account, out var message))
            return Results.BadRequest(message);

        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    [Route("accounts")]
    [HttpPatch]
    async public Task<IResult> UpdateAccount(Account account)
    {
        if (!_ValidateUpdatedAccount(account, out var message))
            return Results.BadRequest(message);

        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    [Route("accounts")]
    [HttpDelete]
    async public Task<IResult> DeleteAccountByObject(Account account)
    {
        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    [Route("accounts/{id:int}")]
    [HttpGet]
    async public Task<IResult> GetAccountById(int id)
    {
        var account = await _dbContext.Accounts.FindAsync(id);
        if (account is null)
            return Results.NotFound();

        return Results.Json(account);
    }

    [Route("accounts/{id:int}")]
    [HttpDelete]
    async public Task<IResult> DeleteAccountById(int id)
    {
        var account = await _dbContext.Accounts.FindAsync(id);
        if (account is null)
            return Results.NotFound();

        _dbContext.Accounts.Remove(account);
        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    [Route("api/accounts/{id:int}")]
    [HttpPatch]
    async public Task<IResult> UpdateAccountById(int id, [FromBody] JsonDocument partOfAccount)
    {
        var account = await _dbContext.Accounts.FindAsync(id);
        if (account is null)
            return Results.NotFound();

        _ChangeAccountFromJson(account, partOfAccount.RootElement);

        if (!_ValidateUpdatedAccount(account, out var message))
            return Results.BadRequest(message);

        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    private void _ChangeAccountFromJson(Account account, JsonElement json)
    {
        if (json.TryGetProperty("AccountNumber", out var accountNumber))
            account.AccountNumber = accountNumber.GetString();
        if (json.TryGetProperty("StartDate", out var startDate))
            account.StartDate = startDate.GetDateTime();
        if (json.TryGetProperty("EndDate", out var endDate))
            account.EndDate = endDate.GetDateTime();
        if (json.TryGetProperty("Address", out var address))
            account.Address = address.GetString();
        if (json.TryGetProperty("Area", out var area))
            account.Area = area.GetDouble();
        if (json.TryGetProperty("ResidentsIds", out var residentsIds))
            account.ResidentsIds = residentsIds.GetString();
    }

    private bool _ValidateNewAccount(Account account, out string message)
    {
        message = _GetErrorMessageOrOk(account, isNewAccount: true);
        return message == "Ok";
    }

    private bool _ValidateUpdatedAccount(Account account, out string message)
    {
        message = _GetErrorMessageOrOk(account, isNewAccount: false);
        return message == "Ok";
    }

    private string _GetErrorMessageOrOk(Account account, bool isNewAccount)
    {
        if (account.Address == "")
            return "Account number is empty";
        if (account.StartDate < new DateTime(1900, 1, 1))
            return "Too early StartDate";
        if (account.EndDate < new DateTime(1900, 1, 1))
            return "Too early StartDate";
        if (account.EndDate <= account.StartDate)
            return "EndDate earlier than StartDate";
        if (account.Address == "")
            return "Emtpty Address";
        if (account.Area <= 0)
            return "Area less or equal to Zero";
        if (!_ValidateResidentsIds(account.ResidentsIds))
            return "Wrong Format of Residents Ids";

        if (isNewAccount)
        {
            if (!_CheckIfIdIsUnique(account))
                return "Already existing account ID";
            if (!_CheckIfAccountNumberIsUnique(account))
                return "Account number already exists";
        }

        return "Ok";
    }

    private bool _CheckIfIdIsUnique(Account account)
    {
        foreach (var compareAccount in _dbContext.Accounts)
            if (account.AccountNumber != compareAccount.AccountNumber)
                if (account.Id == compareAccount.Id)
                    return false;
        return true;
    }

    private bool _CheckIfAccountNumberIsUnique(Account account)
    {
        foreach (var compareAccount in _dbContext.Accounts)
            if (account.Id != compareAccount.Id)
                if (account.AccountNumber == compareAccount.AccountNumber)
                    return false;
        return true;
    }

    private bool _ValidateResidentsIds(string residentIds)
    {
        try
        {
            residentIds
                    .Split(',')
                    .Select(number => int.Parse(number.Trim()))
                    .ToList();
        }
        catch
        {
            return false;
        }

        return true;
    }
}