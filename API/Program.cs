using Core.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Use the configuration
var fruityviceApiBaseUrl = config["FruityviceApi:BaseUrl"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("FruityviceApi", client =>
{
    client.BaseAddress = new Uri(fruityviceApiBaseUrl);
});


builder.Services.AddMemoryCache();
builder.Services.AddTransient<IFruityviceApiClient, FruityviceApiClient>();
builder.Services.AddScoped<IFruitService, FruitService>();
builder.Services.AddLogging();


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
