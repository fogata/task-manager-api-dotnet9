using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Tests.Repositories;

public class UnitOfWorkTests
{
    [Fact]
    public async Task SaveChangesAsync_Should_Save()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("uow_db").Options;

        var context = new AppDbContext(options);
        var uow = new UnitOfWork(context);

        context.Users.Add(new Core.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "teste",
            Role = "Gerente"
        });

        // Act
        await uow.SaveChangesAsync();

        // Assert
        context.Users.Should().HaveCount(1);
    }

}
