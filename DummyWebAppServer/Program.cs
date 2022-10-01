using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .ConfigureKestrel(options =>
    {
        options.ListenAnyIP(8080);
    });

// Add services to the container.

builder.Services.AddControllers();

var app = builder
    .Build();

app.MapControllers();

app.Run();