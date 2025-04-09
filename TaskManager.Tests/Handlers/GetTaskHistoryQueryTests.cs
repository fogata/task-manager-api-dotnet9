using Xunit;
using FluentAssertions;
using TaskManager.Application.UseCases.Queries;

namespace TaskManager.Tests.Queries
{
    public class GetTaskHistoryQueryTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties_Correctly()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            // Act
            var query = new GetTaskHistoryQuery(taskId);

            // Assert
            query.TaskId.Should().Be(taskId);
        }
    }
}
