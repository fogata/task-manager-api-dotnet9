using TaskManager.Core.Enums;

namespace TaskManager.Application.UseCases.Commands;

public record CreateTaskCommand(
    Guid ProjectId,
    string Title,
    string Description,
    DateTime DueDate,
    TaskPriority Priority
);
