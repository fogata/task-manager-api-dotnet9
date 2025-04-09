using FluentValidation.TestHelper;
using TaskManager.Application.UseCases.Queries;
using TaskManager.Application.Validators;

namespace TaskManager.Tests.Validators;

public class GetTaskHistoryQueryValidatorTests
{
    private readonly GetTaskHistoryQueryValidator _validator;

    public GetTaskHistoryQueryValidatorTests()
    {
        _validator = new GetTaskHistoryQueryValidator();
    }

    [Fact]
    public void Should_Pass_When_TaskId_Is_Valid()
    {
        var query = new GetTaskHistoryQuery(Guid.NewGuid());

        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_TaskId_Is_Empty()
    {
        var query = new GetTaskHistoryQuery(Guid.Empty);

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.TaskId)
              .WithErrorMessage("O ID da tarefa é obrigatório");
    }
}
