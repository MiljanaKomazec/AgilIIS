using Microsoft.EntityFrameworkCore;
using URIS_Grupa11.Entities;
using URIS_Grupa11.Helpers;
using URIS_Grupa11.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dodato
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>(); // dodato zbog loggera
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<TeamContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TeamDB"))
           .LogTo(Console.WriteLine, LogLevel.Information);
});
//

//Dodato za front
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Dodato za front
app.UseCors("AllowOrigin");
//

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
