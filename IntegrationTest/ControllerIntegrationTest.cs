using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using Azure;
using dotenv.net;
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
        _app = new SolarWatchWebApplicationFactory();
        _client = _app.CreateClient();
        DotEnv.Load();  
        _output = output;
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables() 
            .Build();
        _tokenService = new TokenService(new JwtSettings { SecretKey = configuration["JwtSecretKey"]});
    }
    
    
    [Fact]
    public async Task TestSolarEndpointIfAuthorized()
    {
        // Get the user token
        var token = GenerateJwtToken();
        // Create the request, add token to the headers
        var request = new HttpRequestMessage(HttpMethod.Get, "/SolarWatch?cityName=Barcelona&date=2024-02-07");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
        }
        response.EnsureSuccessStatusCode();
        // Check if we get the correct data
        var data = await response.Content.ReadFromJsonAsync<Solar>();
        // expceted data:
        var expected = new Solar()
            { Id = 1, Sunrise = "06:30", Sunset = "18:45", Date = new DateOnly(2024, 2, 7), CityId = 1 };
        Assert.True(
            expected.Id == data.Id &&
            expected.CityId == data.CityId &&
            expected.Date == data.Date &&
            expected.Sunrise == data.Sunrise &&
            expected.Sunset == data.Sunset
        );
    }

    [Fact]
    public async Task CityPostTestWithAdminToken()
    {
        var token = GenerateJwtToken("Admin");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/city");
        request.Headers.Add("Authorization", $"Bearer {token}");
        City city = new City(){Id = 100, Name = "Túrkeve", Longitude = 53.532, Latitude = 53.42};
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
        var token = GenerateJwtToken("Admin");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/solar");
        request.Headers.Add("Authorization", $"Bearer {token}");
        Solar solar = new Solar(){City = new City(){Id = 100, Latitude = 34.432, Longitude = 53.53, Name = "Kaposvár"}, CityId = 1, Sunrise = "5:52:52 AM", Sunset = "5:52:52 PM", Id = 100, Date = new DateOnly(2024,12,12)};
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
        var token = GenerateJwtToken("Admin");
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
        var user = new ApplicationUser{UserName = "TestUser", Email = "TestEmail"};
        return _tokenService.CreateToken(user, role);
    }

}