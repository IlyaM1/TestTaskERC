using AccountWebAPI.Database.Models;
using AccountWebAPI.Dtos;

namespace AccountWebAPI.Database.Repositories.Abstractions;

public interface IAccountRepository
{
    public Task<List<Account>> GetAllAccountsAsync();
    public Task<Account?> GetOneAccountAsync(Guid id);
    public Task<Guid> CreateAccount(Account account);
    public Task PatchAccount(Guid accountId, PatchAccountDto patchDto);
    public Task DeleteAccount(Guid accountId);
}
