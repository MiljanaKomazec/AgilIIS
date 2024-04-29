using Microsoft.EntityFrameworkCore;
using UserStory.Data.DataFunctionallity;
using UserStory.Data.DataPP;
using UserStory.Data.DataTask;
using UserStory.Data.DataUserStory;
using UserStory.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using UserStory.Helpers;
using System.Text;
using UserStory.ServiceCalls;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dodajte registraciju AutoMapper-a
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:5242", "http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

builder.Services.AddDbContext<UserStoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserStoryDB"))
           .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<IUserStoryRepository, UserStoryRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IFunctionalityRepository, FunctionalityRepository>();
builder.Services.AddScoped<IPrioritetizationParameterRepository, PrioritetizationParameterRepository>();


builder.Services.AddScoped<ILoggerService, LoggerService>();

builder.Services.AddScoped<IServiceCalls, ServiceCalls>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();