using AccountWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Map("/", () => "This is API server, it isn't rendering pages, it works with data");

app.Run();