using Newtonsoft.Json;
using System.Text;

namespace AccountManagingServer;

public class SearchMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiServerUrl;

    public SearchMiddleware(RequestDelegate next, string apiServerUrl)
    {
        this._next = next;
        this._apiServerUrl = apiServerUrl;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString();
        var method = context.Request.Method;
        var response = context.Response;

        if (path.StartsWith("/search"))
        {
            var accounts = GetAccounts();
            var accountNumberStart = GetAccountNumberStartFromPath(path);

            var accountsThatFit = accounts.Where(account => account.AccountNumber.Contains(accountNumberStart)).ToList();
            await response.WriteAsJsonAsync(accountsThatFit);
        }
        else
            await _next.Invoke(context);
    }

    private List<Account> GetAccounts()
    {
        var task = Task.Run(async () => await HttpRequests.GetRequestAsync($"{_apiServerUrl}/api/accounts"));
        task.Wait();
        var jsonStringFromServer = task.Result;
        var json = JsonConvert.DeserializeObject<List<Account>>(jsonStringFromServer);

        return json;
    }

    private string GetAccountNumberStartFromPath(string path)
    {
        return path.Substring(path.LastIndexOf('/') + 1);
    }
}
