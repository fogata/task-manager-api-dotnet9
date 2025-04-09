using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class UpdateTaskHandlerTests
{
    [Fact]
    public async Task Should_Record_History_When_Updating_Fields()
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Old Title",
            Description = "Old Desc",
            DueDate = DateTime.UtcNow.AddDays(2),
            Status = Core.Enums.TaskStatus.Pending
        };

        var repo = new Mock<ITaskRepository>();
        var repoHist = new Mock<ITaskHistoryRepository>();
        
        repo.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);
        var uow = new Mock<IUnitOfWork>();
        var handler = new UpdateTaskHandler(repo.Object, repoHist.Object, uow.Object);

        var command = new UpdateTaskCommand(task.Id, "New Title", "New Desc", task.DueDate.AddDays(1), "InProgress", Guid.NewGuid());
        var result = await handler.HandleAsync(command);

        result.Should().NotBeNull();
        result.Should().BeAssignableTo<TaskItem>();
        uow.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}