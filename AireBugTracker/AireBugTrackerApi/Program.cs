using DatabaseContext;
using Microsoft.Extensions.Configuration;
using Repositories.Interfaces;
using Repositories.Respositories;
using Services.Interfaces;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
var connectionString = builder.Configuration.GetConnectionString("BugTrackerContext");
builder.Services.AddScoped(s => new BugTrackerContext(connectionString));
builder.Services.AddScoped<IBugRepository, BugRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBugService, BugService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
