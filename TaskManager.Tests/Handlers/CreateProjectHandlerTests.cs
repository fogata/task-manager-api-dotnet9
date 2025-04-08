using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Handlers;

public class CreateProjectHandlerTests
{
    [Fact]
    public async Task Should_Create_Project_When_Valid()
    {
        var repo = new Mock<IProjectRepository>();
        var handler = new CreateProjectHandler(repo.Object, Mock.Of<ILogger<CreateProjectHandler>>());
        var command = new CreateProjectCommand(Guid.NewGuid(), "Alpha");

        var result = await handler.HandleAsync(command);

        result.Should().NotBeNull();
        result.Name.Should().Be("Alpha");
        repo.Verify(r => r.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}