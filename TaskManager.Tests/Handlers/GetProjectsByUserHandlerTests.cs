using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Tests.Unit.UseCases;

public class GetProjectsByUserHandlerTests
{
    [Fact]
    public async Task Deve_Retornar_Projetos_De_Usuario()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mockRepo = new Mock<IProjectRepository>();
        mockRepo.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(new List<Project>
        {
            new() { Id = Guid.NewGuid(), Name = "Projeto A", UserId = userId },
            new() { Id = Guid.NewGuid(), Name = "Projeto B", UserId = userId }
        });

        var handler = new GetProjectsByUserHandler(mockRepo.Object);

        // Act
        var result = await handler.HandleAsync(new(userId));

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(p => p.UserId.Should().Be(userId));
    }
}