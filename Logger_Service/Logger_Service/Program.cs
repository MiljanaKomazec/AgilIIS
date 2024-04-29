using Logger_Service.Data;

var builder = WebApplication.CreateBuilder(args);

NLog.LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dodajte registraciju AutoMapper-a
builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddScoped<ILoggerRepository, LoggerRepository>();



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
