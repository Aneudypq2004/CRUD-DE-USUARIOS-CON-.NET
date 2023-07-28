using WebApi.Models;
using WebApi.Controllers;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost("/", User.newUser);


app.MapGet("/", async () => {

    HttpClient client = new HttpClient();

    var response = await client.GetStringAsync("https://rickandmortyapi.com/api/character/?page=20");

    return response;
});

app.Run();
