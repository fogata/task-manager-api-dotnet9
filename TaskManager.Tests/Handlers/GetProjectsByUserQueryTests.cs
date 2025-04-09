using Xunit;
using FluentAssertions;
using TaskManager.Application.UseCases.Queries;

namespace TaskManager.Tests.Queries
{
    public class GetProjectsByUserQueryTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties_Correctly()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var query = new GetProjectsByUserQuery(userId);

            // Assert
            query.UserId.Should().Be(userId);
        }
    }
}
