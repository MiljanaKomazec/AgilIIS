using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoryPointAPI.Helpers;
using StoryPointAPI.Models;
using StoryPointAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddDbContext<StoryPointContext>(opt =>
    opt.UseInMemoryDatabase("StoryPointList"));*/
builder.Services.AddDbContext<StoryPointContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoryPoint"))
           .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<StoryPointContext>();
builder.Services.AddScoped<PlanningPokerManager>();
builder.Services.AddScoped<StoryPointService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
