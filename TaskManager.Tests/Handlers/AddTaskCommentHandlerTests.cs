using FluentAssertions;
using Microsoft.Extensions.Logging;
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
        var taskId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var task = new TaskItem
        {
            Id = taskId,
            Comments = new List<TaskComment>(),
            UpdateHistory = new List<TaskHistory>()
        };

        var user = new User { Id = userId, Username = "tester", Role = "User" };

        var repo = new Mock<ITaskRepository>();
        var repoUser = new Mock<IUserRepository>();
        var repoComment = new Mock<ITaskCommentRepository>();
        var repoHistory = new Mock<ITaskHistoryRepository>();
        var logmock = new Mock<ILogger<AddTaskCommentHandler>>();

        repo.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(task);
        repoUser.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

        var uow = new Mock<IUnitOfWork>();
        var handler = new AddTaskCommentHandler(repo.Object, repoUser.Object, repoComment.Object, repoHistory.Object, uow.Object, logmock.Object);

        var result = await handler.HandleAsync(new AddTaskCommentCommand(taskId, userId, "Comentario"));

        result.Should().BeAssignableTo<TaskItem>();
        uow.Verify(u => u.SaveChangesAsync(), Times.Once);

    }

}
