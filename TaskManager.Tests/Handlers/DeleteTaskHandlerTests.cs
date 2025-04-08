using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class DeleteTaskHandlerTests
{
    [Fact]
    public async Task Should_Delete_Task_When_Exists()
    {
        var task = new TaskItem { Id = Guid.NewGuid() };
        var repo = new Mock<ITaskRepository>();
        repo.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

        var uow = new Mock<IUnitOfWork>();
        var handler = new DeleteTaskHandler(repo.Object, uow.Object);

        var result = await handler.HandleAsync(new DeleteTaskCommand(task.Id));
        result.Should().BeTrue();
        repo.Verify(r => r.RemoveAsync(task), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}