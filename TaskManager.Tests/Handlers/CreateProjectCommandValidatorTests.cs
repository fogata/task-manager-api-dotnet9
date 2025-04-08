using FluentValidation.TestHelper;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Application.Validators;

namespace TaskManager.Tests.Validators;

public class CreateProjectCommandValidatorTests
{
    private readonly CreateProjectCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var result = _validator.TestValidate(new CreateProjectCommand(Guid.NewGuid(), ""));
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var result = _validator.TestValidate(new CreateProjectCommand(Guid.NewGuid(), "Projeto Alpha"));
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
}