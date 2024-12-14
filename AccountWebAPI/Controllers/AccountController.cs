using AccountWebAPI.Database.Models;
using AccountWebAPI.Database.Repositories.Abstractions;
using AccountWebAPI.Dtos;

using Microsoft.AspNetCore.Mvc;


namespace AccountWebAPI.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountController(IAccountRepository accountRepository) : ControllerBase
{
    [HttpGet]
    public async Task<List<Account>> GetAccounts() => 
        await accountRepository.GetAllAccountsAsync();

    [Route("{id}")]
    [HttpGet]
    public async Task<Account> GetAccountById([FromRoute] Guid id) => 
        await accountRepository.GetOneAccountAsync(id)
            ?? throw new BadHttpRequestException("Аккаунта с таким id нет");

    [HttpPost]
    public async Task<Guid> CreateAccount([FromBody] CreateAccountDto createAccountDto) => 
        await accountRepository.CreateAccount(createAccountDto.Map());

    [Route("{id}")]
    [HttpPatch]
    public async Task PatchAccountById([FromRoute] Guid id, [FromBody] PatchAccountDto patchAccountDto) 
        => await accountRepository.PatchAccount(id, patchAccountDto);

    [Route("{id}")]
    [HttpDelete]
    public async Task DeleteAccount([FromRoute] Guid id)
        => await accountRepository.DeleteAccount(id);
}