using Microsoft.EntityFrameworkCore;
using URIS_TD.Entities;
using URIS_TD.Helpers;
using URIS_TD.InterfaceRepository;
using URIS_TD.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TechnicalDebtContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITechnicalDebtRepository, TechnicalDebtRepository>();
builder.Services.AddScoped<ITypeOfTechnicalDebtRepository, TypeOfTechnicalDebtRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(TechnicalDebtProfile));

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
