using AccountWebAPI.Database.Models;
using AccountWebAPI.Database.Repositories.Abstractions;
using AccountWebAPI.Dtos;

using Microsoft.EntityFrameworkCore;

namespace AccountWebAPI.Database.Repositories.EfImplementation;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<Guid> CreateAccount(Account account)
    {
        context.Accounts.Add(account);
        await context.SaveChangesAsync();

        return account.Id;
    }

    public async Task DeleteAccount(Guid accountId)
    {
        var account = await GetOneAccountAsync(accountId);

        if (account is not null)
        {
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        return await context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetOneAccountAsync(Guid id)
    {
        return await context.Accounts.FirstOrDefaultAsync(acc => acc.Id == id);
    }

    public async Task PatchAccount(Guid accountId, PatchAccountDto patchDto)
    {
        var account = await GetOneAccountAsync(accountId);

        if (account is not null)
        {
            patchDto.Apply(account);
            await context.SaveChangesAsync();
        }
    }
}
