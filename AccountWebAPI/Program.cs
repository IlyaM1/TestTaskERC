using AccountWebAPI.Database;
using AccountWebAPI.Database.Repositories.Abstractions;
using AccountWebAPI.Database.Repositories.EfImplementation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod());


app.Run();