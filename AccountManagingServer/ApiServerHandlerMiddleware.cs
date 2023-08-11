using System.Text;
using System;
using System.Text.Json;

namespace AccountManagingServer;

public class ApiServerHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiServerUrl;

    public ApiServerHandlerMiddleware(RequestDelegate next, string apiServerUrl)
    {
        this._next = next;
        this._apiServerUrl = apiServerUrl;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.ToString();
        var method = context.Request.Method.ToString();
        var response = context.Response;

        if (path.StartsWith("/api"))
        {
            var requestUrl = $"{_apiServerUrl}{path}";
            string jsonFromServer;
            Account account;

            switch (method)
            {
                case "GET":
                    jsonFromServer = await HttpRequests.GetRequestAsync(requestUrl);
                    await response.WriteAsJsonAsync(jsonFromServer);
                    break;
                case "POST":
                    account = _GetRequestBody(context.Request);
                    jsonFromServer = await HttpRequests.PostRequestAsync(requestUrl, JsonSerializer.Serialize(account));
                    await response.WriteAsJsonAsync(jsonFromServer);
                    break;
                case "PATCH":
                    account = _GetRequestBody(context.Request);
                    jsonFromServer = await HttpRequests.PatchRequestAsync(requestUrl, JsonSerializer.Serialize(account));
                    await response.WriteAsJsonAsync(jsonFromServer);
                    break;
                case "DELETE":
                    jsonFromServer = await HttpRequests.DeleteRequestAsync(requestUrl);
                    await response.WriteAsJsonAsync(jsonFromServer);
                    break;
                default:
                    throw new Exception("There are no methods Api server works with");
            }
        }
        else
            await _next.Invoke(context);
    }

    private Account _GetRequestBody(HttpRequest request)
    {
        var jsonTask = request.ReadFromJsonAsync<Account>();
        return jsonTask.Result;
    }
}
