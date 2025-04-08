using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId);
    Task RemoveAsync(TaskItem task);
    Task<int> CountCompletedByUserLast30Days(Guid userId);
}
