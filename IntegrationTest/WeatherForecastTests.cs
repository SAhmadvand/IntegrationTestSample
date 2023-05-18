using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTest;

public class WeatherForecastTests : IClassFixture<BaseWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly BaseWebApplicationFactory<Program> _factory;

    public WeatherForecastTests(BaseWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task TestGetMethod()
    {
        var response = await _client.GetAsync("/WeatherForecast/");
        Assert.True(response.IsSuccessStatusCode);
    }
}