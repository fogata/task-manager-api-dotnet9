using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class DeleteProjectHandler
{
    private readonly IProjectRepository _projectRepo;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectHandler(IProjectRepository projectRepo, IUnitOfWork unitOfWork)
    {
        _projectRepo = projectRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Message)> HandleAsync(DeleteProjectCommand command)
    {
        var project = await _projectRepo.GetByIdAsync(command.ProjectId);
        if (project is null) return (false, "Projeto não encontrado");

        var hasPending = project.Tasks.Any(t => t.Status == Core.Enums.TaskStatus.Pending);
        if (hasPending)
            return (false, "O projeto possui tarefas pendentes e não pode ser removido.");

        await _projectRepo.RemoveAsync(project);
        await _unitOfWork.SaveChangesAsync();
        return (true, "Projeto removido com sucesso.");
    }
}
