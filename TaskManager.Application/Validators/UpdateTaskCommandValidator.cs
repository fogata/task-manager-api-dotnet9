using FluentValidation;
using TaskManager.Application.UseCases.Commands;

namespace TaskManager.Application.Validators;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título é obrigatório")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("A data de vencimento deve ser futura");

        RuleFor(x => x.Status)
            .Must(s => Enum.TryParse<Core.Enums.TaskStatus>(s, true, out _))
            .WithMessage("Status inválido");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("Usuário responsável é obrigatório");
    }
}
