using FluentValidation;
using TaskManager.Application.UseCases.Commands;

namespace TaskManager.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Dto.Username)
            .NotEmpty().WithMessage("O nome de usuário é obrigatório")
            .MaximumLength(50).WithMessage("O nome de usuário deve ter no máximo 50 caracteres");
    }
}
