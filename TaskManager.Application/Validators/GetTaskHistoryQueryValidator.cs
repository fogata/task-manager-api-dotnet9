using FluentValidation;
using TaskManager.Application.UseCases.Queries;

namespace TaskManager.Application.Validators;

public class GetTaskHistoryQueryValidator : AbstractValidator<GetTaskHistoryQuery>
{
    public GetTaskHistoryQueryValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEqual(Guid.Empty).WithMessage("O ID da tarefa é obrigatório");
    }
}
