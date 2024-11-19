using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;
using SolarWatch.Services;
using SolarWatch.Services.JsonParsers;
using SolarWatch.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICityParser, CityProcessor>();
builder.Services.AddSingleton<ICityDataProvider, CityApiReader>();
builder.Services.AddSingleton<ISolarParser, SolarProcessor>();
builder.Services.AddSingleton<ISolarInfoProvider, SolarInfoReader>();
builder.Services.AddScoped<ISolarRepository, SolarRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddDbContext<SolarApiContext>(options =>
{
    options.UseSqlServer(connectionString);
});

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
