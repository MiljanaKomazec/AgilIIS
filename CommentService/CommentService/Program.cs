using CommentService.Data;
using CommentService.Entiti;
using CommentService.Helper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<ILoggerService, LoggerService>();

builder.Services.AddDbContext<CommentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommentDB"))
       .LogTo(Console.WriteLine, LogLevel.Information);
});

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
