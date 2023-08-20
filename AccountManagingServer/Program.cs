using AccountManagingServer;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var serverUrl = app.Configuration.GetValue(typeof(string), "ApiServerUrl").ToString();

app.Map("/", async (context) => await context.Response.SendFileAsync("html/index.html"));
app.Map("/{id}", async (context) => await context.Response.SendFileAsync("html/detailed_account.html"));
app.Map("/{id}/edit", async (context) => await context.Response.SendFileAsync("html/edit_account.html"));
app.Map("/new", async (context) => await context.Response.SendFileAsync("html/new_account.html"));

app.UseMiddleware<ApiServerHandlerMiddleware>(serverUrl);
app.UseMiddleware<SearchMiddleware>(serverUrl);

app.Run();  