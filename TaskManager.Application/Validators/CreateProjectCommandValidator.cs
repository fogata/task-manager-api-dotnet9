using FluentValidation;
using TaskManager.Application.UseCases.Commands;

namespace TaskManager.Application.Validators;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do projeto é obrigatório")
            .MaximumLength(100).WithMessage("O nome do projeto deve ter no máximo 100 caracteres");
    }
}
