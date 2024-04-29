using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using GatewayAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});
builder.Configuration.AddJsonFile("Properties/Ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddScoped<ILoggerService, LoggerService>();

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true

                    };
                });

var app = builder.Build();

app.UseCors("AllowOrigin");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

await app.UseOcelot();



app.Run();
