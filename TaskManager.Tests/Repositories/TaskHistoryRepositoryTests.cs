using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Tests.Repositories;

public class TaskHistoryRepositoryTests
{
    [Fact]
    public async Task GetByTaskId_Should_Return_History()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new AppDbContext(options);
        var repository = new TaskHistoryRepository(context);

        var taskId = Guid.NewGuid();
        context.TaskHistories.AddRange(new List<TaskHistory>
        {
            new TaskHistory { Id = Guid.NewGuid(), TaskItemId = taskId, Field = "Status", OldValue = "Pending", NewValue = "Done" }
        });
        await context.SaveChangesAsync();

        var result = await repository.GetByTaskIdAsync(taskId);
        result.Should().HaveCount(1);
    }
}
