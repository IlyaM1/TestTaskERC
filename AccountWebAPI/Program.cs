using AccountWebAPI;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    var body = context.Request.Body;
    var path = context.Request.Path;
    var buffer = context.Request.BodyReader.ReadAsync().Result.Buffer;
    var requestBody = Encoding.UTF8.GetString(buffer);
    Console.WriteLine($"---------------------------------\nPath {path}, body: {body}, method: {context.Request.Method}, requestBody: {requestBody}\n---------------------------------");
    if (context.Request.Method == "POST")
    {
        context.Request.Headers.ContentType.ToArray().AsEnumerable().Select(s =>
        {
            Console.WriteLine(s);
            return s;
        });
        try
        {
            var task = await context.Request.ReadFromJsonAsync<Account>();
            Console.WriteLine(task.Id);
        }
        catch
        {
            Console.WriteLine("\n\n\nCan't Cast to Account!\n\n\n");
        }
    }
    context.Request.Body.Position = 0;
    await next.Invoke();
    Console.WriteLine($"---------------------------------\nCompleted response: body: {context.Response.Body} , StatusCode: {context.Response.StatusCode}\n---------------------------------");
});

app.MapControllers();

app.Map("/", () => "This is API server, it isn't rendering pages, it works with data");

app.Run();