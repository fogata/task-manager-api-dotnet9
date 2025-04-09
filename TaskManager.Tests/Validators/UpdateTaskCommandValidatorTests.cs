using FluentValidation.TestHelper;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Application.Validators;

namespace TaskManager.Tests.Validators;

public class UpdateTaskCommandValidatorTests
{
    private readonly UpdateTaskCommandValidator _validator;

    public UpdateTaskCommandValidatorTests()
    {
        _validator = new UpdateTaskCommandValidator();
    }

    [Fact]
    public void Should_Pass_When_Command_Is_Valid()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: "Tarefa válida",
            Description: "Descrição válida",
            DueDate: DateTime.UtcNow.AddDays(1),
            Status: "Completed",
            UserId: Guid.NewGuid()
        );

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_When_Title_Is_Empty()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: "",
            Description: "desc",
            DueDate: DateTime.UtcNow.AddDays(1),
            Status: "Pending",
            UserId: Guid.NewGuid()
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title)
              .WithErrorMessage("O título é obrigatório");
    }

    [Fact]
    public void Should_Fail_When_Title_Exceeds_Limit()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: new string('A', 101),
            Description: "desc",
            DueDate: DateTime.UtcNow.AddDays(1),
            Status: "Pending",
            UserId: Guid.NewGuid()
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Should_Fail_When_Description_Is_Empty()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: "Título",
            Description: "",
            DueDate: DateTime.UtcNow.AddDays(1),
            Status: "Pending",
            UserId: Guid.NewGuid()
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Description)
              .WithErrorMessage("A descrição é obrigatória");
    }

    [Fact]
    public void Should_Fail_When_DueDate_Is_Past()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: "Título",
            Description: "desc",
            DueDate: DateTime.UtcNow.AddDays(-1),
            Status: "Pending",
            UserId: Guid.NewGuid()
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.DueDate)
              .WithErrorMessage("A data de vencimento deve ser futura");
    }

    [Fact]
    public void Should_Fail_When_Status_Is_Invalid()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: "Título",
            Description: "desc",
            DueDate: DateTime.UtcNow.AddDays(1),
            Status: "InvalidStatus",
            UserId: Guid.NewGuid()
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Status)
              .WithErrorMessage("Status inválido");
    }

    [Fact]
    public void Should_Fail_When_UserId_Is_Empty()
    {
        var command = new UpdateTaskCommand(
            TaskId: Guid.NewGuid(),
            Title: "Título",
            Description: "desc",
            DueDate: DateTime.UtcNow.AddDays(1),
            Status: "Pending",
            UserId: Guid.Empty
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.UserId)
              .WithErrorMessage("Usuário responsável é obrigatório");
    }
}
