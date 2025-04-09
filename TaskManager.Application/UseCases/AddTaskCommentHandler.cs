using Microsoft.Extensions.Logging;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.UseCases;

public class AddTaskCommentHandler(
    ITaskRepository _taskRepository,
    IUserRepository _userRepository,
    ITaskCommentRepository _commentRepository,
    ITaskHistoryRepository _historyRepository,
    IUnitOfWork _unitOfWork,
    ILogger<AddTaskCommentHandler> _logger)
{
   
    public async Task<TaskItem?> HandleAsync(AddTaskCommentCommand command)
    {
        var task = await _taskRepository.GetByIdAsync(command.TaskId);
        if (task is null)
            throw new NotFoundException("Tarefa não encontrada.");

        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
            throw new NotFoundException("Usuário não encontrado.");

        var comment = new TaskComment
        {
            Id = Guid.NewGuid(),
            TaskItemId = command.TaskId,
            UserId = command.UserId,
            Content = command.Content,
            CreatedAt = DateTime.UtcNow
        };

        var history = new TaskHistory
        {
            Id = Guid.NewGuid(),
            TaskItemId = command.TaskId,
            Field = "Comment",
            OldValue = string.Empty,
            NewValue = command.Content,
            UpdatedAt = DateTime.UtcNow,
            UserId = command.UserId
        };

        await _commentRepository.AddAsync(comment);
        await _historyRepository.AddAsync(history);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Comentário adicionado à tarefa {TaskId} por {UserId}", command.TaskId, command.UserId);

        return task;
    }
}
