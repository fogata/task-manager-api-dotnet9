using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class AddTaskCommentHandler
{
    private readonly ITaskRepository _taskRepo;
    private readonly IUnitOfWork _unitOfWork;

    public AddTaskCommentHandler(ITaskRepository taskRepo, IUnitOfWork unitOfWork)
    {
        _taskRepo = taskRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> HandleAsync(AddTaskCommentCommand command)
    {
        var task = await _taskRepo.GetByIdAsync(command.TaskId);
        if (task is null) return false;

        var comment = new TaskComment
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            CreatedAt = DateTime.UtcNow,
            UserId = command.UserId,
            TaskItemId = task.Id
        };

        var history = new TaskUpdateHistory
        {
            Id = Guid.NewGuid(),
            TaskItemId = task.Id,
            Field = "Comment",
            OldValue = "",
            NewValue = command.Content,
            UpdatedAt = DateTime.UtcNow,
            UserId = command.UserId
        };

        task.Comments.Add(comment);
        task.UpdateHistory.Add(history);

        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
