using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class AddTaskCommentHandlerTests
{
    [Fact]
    public async Task Should_Add_Comment_And_History()
    {
        var task = new TaskItem { Id = Guid.NewGuid() };

        var repo = new Mock<ITaskRepository>();
        repo.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

        var uow = new Mock<IUnitOfWork>();
        var handler = new AddTaskCommentHandler(repo.Object, uow.Object);

        var result = await handler.HandleAsync(new AddTaskCommentCommand(task.Id, Guid.NewGuid(), "Comentario"));

        result.Should().BeTrue();
        task.Comments.Should().ContainSingle();
        task.UpdateHistory.Should().ContainSingle();
        uow.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}