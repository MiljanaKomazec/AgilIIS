using Grupa11_Calendar.Entities;
using Grupa11_Calendar.Helpers;
using Grupa11_Calendar.InterfaceRepository;
using Grupa11_Calendar.Repository;
using Grupa11_Calendar.ServiceCalls;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dodato
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>(); // dodato zbog loggera
builder.Services.AddScoped<IServiceCalls, ServiceCalls>(); // dodato zbog usera
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<CalendarContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CalendarDB"))
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
