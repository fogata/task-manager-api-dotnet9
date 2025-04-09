using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TaskManager.Tests.Integration.Infrastructure;

namespace TaskManager.Tests.Integration.Endpoints;

public class UserEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UserEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Usuario_Com_Sucesso()
    {
        var payload = new { Username = "ana.integration", Role = "Gerente" };

        var response = await _client.PostAsJsonAsync("/users", payload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
