using TaskManager.Application.UseCases.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetTasksByProjectHandler
{
    private readonly ITaskRepository _repository;

    public GetTasksByProjectHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> HandleAsync(GetTasksByProjectQuery query)
    {
        return await _repository.GetByProjectIdAsync(query.ProjectId);
    }
}
