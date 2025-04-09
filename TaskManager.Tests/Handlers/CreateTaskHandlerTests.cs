using Microsoft.Extensions.Logging;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class CreateTaskHandlerTests
{
    [Fact]
    public async Task Should_Throw_When_Project_Not_Found()
    {
        var taskRepo = new Mock<ITaskRepository>();
        var projectRepo = new Mock<IProjectRepository>();
        var handler = new CreateTaskHandler(taskRepo.Object, projectRepo.Object, Mock.Of<ILogger<CreateTaskHandler>>());

        var command = new CreateTaskCommand(Guid.NewGuid(), "Bug", "Fix", DateTime.UtcNow.AddDays(1), TaskPriority.Alta);
        await Assert.ThrowsAsync<Exception>(() => handler.HandleAsync(command));
    }
}