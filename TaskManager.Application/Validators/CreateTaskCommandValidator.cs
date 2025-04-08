using FluentValidation;
using TaskManager.Application.UseCases.Commands;

namespace TaskManager.Application.Validators;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título da tarefa é obrigatório")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição da tarefa é obrigatória");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("A data de vencimento deve ser futura");
    }
}
