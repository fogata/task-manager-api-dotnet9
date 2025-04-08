using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Data;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Projects
            .Where(p => p.UserId == userId)
            .Include(p => p.Tasks)
            .ToListAsync();
    }

    public async Task<int> CountTasksAsync(Guid projectId)
    {
        return await _context.TaskItems.CountAsync(t => t.ProjectId == projectId);
    }

    public async Task RemoveAsync(Project project)
    {
        _context.Projects.Remove(project);
        await Task.CompletedTask;
    }

}
