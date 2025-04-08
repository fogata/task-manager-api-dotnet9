using FluentValidation;
using TaskManager.Application.UseCases.Commands;

namespace TaskManager.Application.Validators;

public class AddTaskCommentCommandValidator : AbstractValidator<AddTaskCommentCommand>
{
    public AddTaskCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("O comentário não pode estar vazio")
            .MaximumLength(500).WithMessage("O comentário pode ter no máximo 500 caracteres");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("O ID do usuário é obrigatório");

        RuleFor(x => x.TaskId)
            .NotEqual(Guid.Empty).WithMessage("O ID da tarefa é obrigatório");
    }
}
