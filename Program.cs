using WebApi.Models;
using WebApi.Controllers;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
