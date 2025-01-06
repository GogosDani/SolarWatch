using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SolarWatch.Contracts;
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
        _tokenService = new TokenService(new JwtSettings { SecretKey = "seckey"});
        _output = output;
    }
    
    [Fact]
    public async Task TestSolarEndpointIfUnAuthorized()
    {
        var response = await _client.GetAsync("/SolarWatch?cityName=London&date=2025-01-01");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task TestSolarEndpointIfAuthorized()
    {
        var token = GenerateJwtToken();
        _output.WriteLine($"Generated Token: {token}");
        var request = new HttpRequestMessage(HttpMethod.Get, "/SolarWatch?cityName=London&date=2025-01-01");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _client.SendAsync(request);
        foreach (var header in _client.DefaultRequestHeaders)
        {
            _output.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");
        }
        response.EnsureSuccessStatusCode();
    }
    
    
    private string GenerateJwtToken()
    {
        var user = new IdentityUser(){UserName = "TestUser", Email = "TestEmail"};
        return _tokenService.CreateToken(user, "User");
    }

}