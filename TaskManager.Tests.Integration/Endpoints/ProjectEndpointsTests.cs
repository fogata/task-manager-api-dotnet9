using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace TaskManager.Tests.Integration.Endpoints;

public class ProjectEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProjectEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Projeto_Com_Sucesso()
    {
        var user = new { Username = "projetouser", Role = "Gerente" };
        var userResponse = await _client.PostAsJsonAsync("/users", user);
        var createdUser = await userResponse.Content.ReadFromJsonAsync<UserResponse>();

        var response = await _client.PostAsJsonAsync($"/users/{createdUser!.Id}/projects","Projeto Teste");

        if (response.StatusCode != HttpStatusCode.Created)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro {response.StatusCode}: {error}");
        }


        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var project = await response.Content.ReadFromJsonAsync<ProjectResponse>();
        project.Should().NotBeNull();
        project!.Name.Should().Be("Projeto Teste");
    }

    private class UserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    private class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}