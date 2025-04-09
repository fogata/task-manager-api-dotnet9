using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Unit.UseCases;

public class GetTasksByProjectHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_Tarefas_De_Projeto()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var mockRepo = new Mock<ITaskRepository>();
        mockRepo.Setup(r => r.GetByProjectIdAsync(projectId)).ReturnsAsync(new List<TaskItem>
        {
            new() { Id = Guid.NewGuid(), Title = "Tarefa 1", ProjectId = projectId },
            new() { Id = Guid.NewGuid(), Title = "Tarefa 2", ProjectId = projectId }
        });

        var handler = new GetTasksByProjectHandler(mockRepo.Object);

        // Act
        var result = await handler.HandleAsync(new(projectId));

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(t => t.ProjectId.Should().Be(projectId));
    }
}