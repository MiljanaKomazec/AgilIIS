using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Sprint.Entities;
using Sprint.Data.DataSprint;
using Sprint.Data.DataBacklog;
using Sprint.Data.DataBacklogItem;
using System.Net;
using Sprint.Data.DataPOBI;
using AutoMapper;
using Sprint.Helper;
using Sprint.ServiceCalls;

var builder = WebApplication.CreateBuilder(args);

/*
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build(); */

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

builder.Services.AddDbContext<SprintContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SprintDB"))
           .LogTo(Console.WriteLine, LogLevel.Information);
    });



    builder.Services.AddScoped<ISprintRepository, SprintRepository>();
    builder.Services.AddScoped<IBacklogRepository, BacklogRepository>();
    builder.Services.AddScoped<IBacklogItemRepository, BacklogItemRepository>();
    builder.Services.AddScoped<IPhaseOfBacklogItemRepository, PhaseOfBacklogItemRepository>();

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