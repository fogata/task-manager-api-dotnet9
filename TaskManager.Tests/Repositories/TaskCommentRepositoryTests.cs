using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Tests.Repositories;

public class TaskCommentRepositoryTests
{
    [Fact]
    public async Task Add_Should_Add_Comment()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new AppDbContext(options);
        var repository = new TaskCommentRepository(context);

        var task = new TaskItem { Id = Guid.NewGuid(), Title = "Task" };
        var comment = new TaskComment { Id = Guid.NewGuid(), TaskItemId = task.Id, Content = "Test", UserId = Guid.NewGuid() };

        context.TaskItems.Add(task);
        await repository.AddAsync(comment);

        await context.SaveChangesAsync();

        var result = await context.TaskComments.FirstOrDefaultAsync();
        result.Should().NotBeNull();
        result.Content.Should().Be("Test");
    }
}
