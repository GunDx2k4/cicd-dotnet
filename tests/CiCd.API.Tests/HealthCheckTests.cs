using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;

namespace CiCd.API.Tests;

public class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthCheckTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHealth_WhenApplicationRuns_ReturnsOkStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetHealth_WhenCalled_ReturnsHealthyStatusInBody()
    {
        // Act
        var response = await _client.GetAsync("/health");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("\"status\":\"Healthy\"", content);
        Assert.Contains("\"service\":\"CiCd.API\"", content);
    }
}
