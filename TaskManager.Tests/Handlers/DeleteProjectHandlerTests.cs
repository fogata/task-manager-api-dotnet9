using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task Should_Not_Delete_Project_If_Has_Pending_Tasks()
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Tasks = new List<TaskItem> { new() { Status = Core.Enums.TaskStatus.Pending } }
        };

        var repo = new Mock<IProjectRepository>();
        repo.Setup(r => r.GetByIdAsync(project.Id)).ReturnsAsync(project);

        var handler = new DeleteProjectHandler(repo.Object, Mock.Of<IUnitOfWork>());
        var (success, message) = await handler.HandleAsync(new DeleteProjectCommand(project.Id));

        success.Should().BeFalse();
        message.Should().Contain("pendentes");
    }
}