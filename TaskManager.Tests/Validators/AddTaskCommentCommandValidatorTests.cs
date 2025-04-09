using FluentValidation.TestHelper;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Application.Validators;

namespace TaskManager.Tests.Validators
{
    public class AddTaskCommentCommandValidatorTests
    {
        private readonly AddTaskCommentCommandValidator _validator;

        public AddTaskCommentCommandValidatorTests()
        {
            _validator = new AddTaskCommentCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new AddTaskCommentCommand(
                TaskId: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                Content: "Comentário válido"
            );

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Fail_When_Content_Is_Empty()
        {
            var command = new AddTaskCommentCommand(
                TaskId: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                Content: ""
            );

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Content)
                  .WithErrorMessage("O comentário não pode estar vazio");
        }

        [Fact]
        public void Should_Fail_When_Content_Exceeds_Max_Length()
        {
            var command = new AddTaskCommentCommand(
                TaskId: Guid.NewGuid(),
                UserId: Guid.NewGuid(),
                Content: new string('A', 501)
            );

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Content)
                  .WithErrorMessage("O comentário pode ter no máximo 500 caracteres");
        }

        [Fact]
        public void Should_Fail_When_UserId_Is_Empty()
        {
            var command = new AddTaskCommentCommand(
                TaskId: Guid.NewGuid(),
                UserId: Guid.Empty,
                Content: "Comentário válido"
            );

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserId)
                  .WithErrorMessage("O ID do usuário é obrigatório");
        }

        [Fact]
        public void Should_Fail_When_TaskId_Is_Empty()
        {
            var command = new AddTaskCommentCommand(
                TaskId: Guid.Empty,
                UserId: Guid.NewGuid(),
                Content: "Comentário válido"
            );

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.TaskId)
                  .WithErrorMessage("O ID da tarefa é obrigatório");
        }
    }
}
