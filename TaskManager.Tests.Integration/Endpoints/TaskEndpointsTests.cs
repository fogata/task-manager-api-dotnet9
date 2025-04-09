using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace TaskManager.Tests.Integration.Endpoints;

public class TaskEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TaskEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Criar_Tarefa_Em_Projeto()
    {
        var user = new { Username = "taskuser", Role = "Gerente" };
        var userResponse = await _client.PostAsJsonAsync("/users", user);
        var createdUser = await userResponse.Content.ReadFromJsonAsync<UserResponse>();

        var projectResponse = await _client.PostAsJsonAsync($"/users/{createdUser!.Id}/projects", "Projeto com Tarefa");

        var createdProject = await projectResponse.Content.ReadFromJsonAsync<ProjectResponse>();

        var task = new { Title = "Minha Tarefa", Description = "Descrição", Priority = "Alta", DueDate = DateTime.UtcNow.AddDays(1) };
        var taskResponse = await _client.PostAsJsonAsync($"/projects/{createdProject!.Id}/tasks", task);
        if (taskResponse.StatusCode != HttpStatusCode.Created)
        {
            var error = await taskResponse.Content.ReadAsStringAsync();
            throw new Exception($"Erro {taskResponse.StatusCode}: {error}");
        }

        taskResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdTask = await taskResponse.Content.ReadFromJsonAsync<TaskResponse>();

        createdTask.Should().NotBeNull();
        createdTask!.Title.Should().Be(task.Title);
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

    private class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
    }
}