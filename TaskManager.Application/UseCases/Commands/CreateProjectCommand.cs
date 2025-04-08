namespace TaskManager.Application.UseCases.Commands;

public record CreateProjectCommand(Guid UserId, string Name);
