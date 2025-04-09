using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces;

public interface ITaskCommentRepository
{
    Task AddAsync(TaskComment comment);
    Task<IEnumerable<TaskComment>> GetByTaskIdAsync(Guid taskId);
}

