namespace TaskManager.Application.UseCases.Commands;

public record AddTaskCommentCommand(
    Guid TaskId,
    Guid UserId,
    string Content
);
