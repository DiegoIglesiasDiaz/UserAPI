using Microsoft.EntityFrameworkCore;
using UserAPI;
using UserAPI.Application;
using UserAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container based on environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseInMemoryDatabase("UserDb")); // InMemoryDatabase for Dev environment
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseSqlServer(connectionString)); // Use SQL Server in
                                                 // uction
}

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
