using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SolarWatch;
using SolarWatch.Contracts;
using SolarWatch.Data;
using SolarWatch.Services.Authentication;
using Xunit.Abstractions;

namespace IntegrationTest;

[Collection("IntegrationTests")]
public class ControllerIntegrationTest
{
    
    private readonly ITestOutputHelper _output;
    private readonly SolarWatchWebApplicationFactory _app;
    private readonly HttpClient _client;
    private readonly ITokenService _tokenService;

    public ControllerIntegrationTest(ITestOutputHelper output)
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<ControllerIntegrationTest>(); 
        var configuration = builder.Build();
        var secretKey = configuration["JWT_SECRET_KEY"];
       
        _app = new SolarWatchWebApplicationFactory();
        _client = _app.CreateClient();
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("Couldn't find jwt secret key!");
        }
        _tokenService = new TokenService(new JwtSettings { SecretKey = secretKey});
        _output = output;
    }
    
    
    [Fact]
    public async Task TestSolarEndpointIfAuthorized()
    {
        var solar = new Solar()
        {
            Id = 1, City = new City() { Id = 1, Latitude = 555.53, Longitude = 53.53, Name = "London" }, CityId = 1,
            Date = new DateOnly(2025, 01, 01), Sunrise = "5:52:52 AM", Sunset = "5:52:52 PM"
        };
        // Add a solar data into the in-memory DB
        using (var scope = _app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
            context.Solars.Add(solar);
            await context.SaveChangesAsync();
        }
        // Get the user token
        var token = GenerateJwtToken();
        // Create the request, add token to the headers
        var request = new HttpRequestMessage(HttpMethod.Get, "/SolarWatch?cityName=London&date=2025-01-01");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
        }
        response.EnsureSuccessStatusCode();
        // Check if we get the correct data
        var data = await response.Content.ReadFromJsonAsync<Solar>();
        Assert.True(
            solar.Id == data.Id &&
            solar.CityId == data.CityId &&
            solar.Date == data.Date &&
            solar.Sunrise == data.Sunrise &&
            solar.Sunset == data.Sunset
        );
    }

    [Fact]
    public async Task CityPostTestWithAdminToken()
    {
        var token = GenerateJwtToken("admin");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/city");
        request.Headers.Add("Authorization", $"Bearer {token}");
        City city = new City(){Id = 1, Name = "Túrkeve", Longitude = 53.532, Latitude = 53.42};
        string json = System.Text.Json.JsonSerializer.Serialize(city);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
        }
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SolarPostWithAdminToken()
    {
        var token = GenerateJwtToken("admin");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/solar");
        request.Headers.Add("Authorization", $"Bearer {token}");
        Solar solar = new Solar(){City = new City(){Id = 1, Latitude = 34.432, Longitude = 53.53, Name = "Kaposvár"}, CityId = 1, Sunrise = "5:52:52 AM", Sunset = "5:52:52 PM", Id = 1, Date = new DateOnly(2024,12,12)};
        string jsonForBody = System.Text.Json.JsonSerializer.Serialize(solar);
        request.Content = new StringContent(jsonForBody, Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
        }
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SolarDeleteWithAdminToken()
    {
        using (var scope = _app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
            context.Solars.Add(new Solar(){Id = 1, City = new City(){Id = 1, Latitude = 555.53, Longitude = 53.53, Name = "Kecskemét"}, CityId = 1, Date = new DateOnly(1931,12,12), Sunrise = "5:52:52 AM", Sunset = "5:52:52 PM"});
            await context.SaveChangesAsync();
        }
        var token = GenerateJwtToken("admin");
        var request = new HttpRequestMessage(HttpMethod.Delete, "/api/solar/1");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
        }
        response.EnsureSuccessStatusCode();
    }
    
    
    private string GenerateJwtToken(string role = "User")
    {
        var user = new IdentityUser(){UserName = "TestUser", Email = "TestEmail"};
        return _tokenService.CreateToken(user, role);
    }

}