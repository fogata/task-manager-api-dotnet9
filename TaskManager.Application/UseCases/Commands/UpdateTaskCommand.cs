namespace TaskManager.Application.UseCases.Commands;

public record UpdateTaskCommand(
    Guid TaskId,
    string Title,
    string Description,
    DateTime DueDate,
    string Status,
    Guid UserId
);
