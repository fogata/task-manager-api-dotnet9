using TaskManager.Application.UseCases.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetTasksByProjectHandler(ITaskRepository _repository)
{
    public async Task<IEnumerable<TaskItem>> HandleAsync(GetTasksByProjectQuery query)
    {
        return await _repository.GetByProjectIdAsync(query.ProjectId);
    }
}
