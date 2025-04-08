using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class GetPerformanceReportHandlerTests
{
    [Fact]
    public async Task Should_Return_Completed_Task_Count_Per_User()
    {
        var user = new User { Id = Guid.NewGuid(), Username = "ana" };

        var userRepo = new Mock<IUserRepository>();
        userRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new[] { user });

        var taskRepo = new Mock<ITaskRepository>();
        taskRepo.Setup(r => r.CountCompletedByUserLast30Days(user.Id)).ReturnsAsync(3);

        var handler = new GetPerformanceReportHandler(taskRepo.Object, userRepo.Object);
        var result = (await handler.HandleAsync()).ToList();

        result.Should().HaveCount(1);
        result[0].Should().BeEquivalentTo(new
        {
            user.Id,
            user.Username,
            CompletedTasksLast30Days = 3
        });
    }
}