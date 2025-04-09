namespace TaskManager.Application.DTOs;

public record TaskResponseDto(Guid Id, string Title, string Priority, Guid ProjectId);

