using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project);
    Task<Project?> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId);
    Task<int> CountTasksAsync(Guid projectId);
    Task RemoveAsync(Project project);
}
