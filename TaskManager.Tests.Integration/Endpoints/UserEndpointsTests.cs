using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TaskManager.Api;

namespace TaskManager.Tests.Integration.Endpoints;

public class UserEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UserEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Usuario_Com_Sucesso()
    {
        var payload = new { Username = "ana.integration", Role = "Gerente" };

        var response = await _client.PostAsJsonAsync("/users", payload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var user = await response.Content.ReadFromJsonAsync<UserResponse>();
        user.Should().NotBeNull();
        user!.Username.Should().Be(payload.Username);
        user.Role.Should().Be(payload.Role);
    }

    private class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}