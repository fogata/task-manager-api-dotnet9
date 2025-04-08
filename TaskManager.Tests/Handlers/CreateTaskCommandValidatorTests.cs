using FluentValidation.TestHelper;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Application.Validators;

namespace TaskManager.Tests.Validators;

public class CreateTaskCommandValidatorTests
{
    private readonly CreateTaskCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Fields_Are_Invalid()
    {
        var command = new CreateTaskCommand(Guid.NewGuid(), "", "", DateTime.UtcNow.AddDays(-1), Core.Enums.TaskPriority.High);
        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Title);
        result.ShouldHaveValidationErrorFor(x => x.Description);
        result.ShouldHaveValidationErrorFor(x => x.DueDate);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid()
    {
        var command = new CreateTaskCommand(Guid.NewGuid(), "Bug", "Fix it", DateTime.UtcNow.AddDays(1), Core.Enums.TaskPriority.Medium);
        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}