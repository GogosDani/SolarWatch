namespace IntegrationTest;

[Collection("IntegrationTests")]
public class ControllerIntegrationTest
{
    private readonly SolarWatchWebApplicationFactory _app;
    private readonly HttpClient _client;

    public ControllerIntegrationTest()
    {
        _app = new SolarWatchWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task TestEndpoint()
    {
        var response = await _client.GetAsync("http://localhost:");
        response.EnsureSuccessStatusCode();
        
    }
}