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
        _tokenService = new TokenService(new JwtSettings { SecretKey = "396b822abe3e785ebcc87ad06edeefa28fe6f51328fee0de762f40ffd821aef46859b74824d13e076ad6b12ab711839cd30ca2bfa1bf27b75abe39aa56f53fdfbdd29fa821edfdfb4b8e014ab8ec6454f2a2d594b16dfdeab9303c04d3c60664d3ad4de8e6312f6981d1ddf10ad4bc8412b1042a46c155b380077a3f9158cd587b647a615f906d82a06478f3a39324e957d77a3fc53b236986262a14ae85949ce54c2cbeb96558ace00ed0bce8ace2d3f25616a27b1bdd5858ef12e599811e6addb403d11d656fcf436edc4c8042c13ddccea2977a1e6de37789675d5df0bdca130418b31d9ac7d6a71b2beb89f69ab5a1ba7d40aa6b68161e6fefbd6dce0d8a"});
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
        _output.WriteLine("Headers in the request:");
        foreach (var header in request.Headers)
        {
            _output.WriteLine($"Key: {header.Key}, Value: {string.Join(", ", header.Value)}");
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