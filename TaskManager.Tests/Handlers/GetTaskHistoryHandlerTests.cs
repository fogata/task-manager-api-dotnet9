using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Unit.UseCases;

public class GetTaskHistoryHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_Historico_De_Tarefa()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var mockRepo = new Mock<ITaskHistoryRepository>();
        mockRepo.Setup(r => r.GetByTaskIdAsync(taskId)).ReturnsAsync(new List<TaskHistory>
        {
            new() { Id = Guid.NewGuid(),  TaskItemId = taskId, Field = "Status", OldValue = "Pending", NewValue = "Done" }
        });

        var handler = new GetTaskHistoryHandler(mockRepo.Object);

        // Act
        var result = await handler.HandleAsync(new(taskId));

        // Assert
        result.Should().HaveCount(1);
        result.First().Field.Should().Be("Status");
    }
}