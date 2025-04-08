using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Data;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task)
    {
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.TaskItems
            .Include(t => t.Comments)
            .Include(t => t.UpdateHistory)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(Guid projectId)
    {
        return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task RemoveAsync(TaskItem task)
    {
        _context.TaskItems.Remove(task);
        await Task.CompletedTask;
    }

    public async Task<int> CountCompletedByUserLast30Days(Guid userId)
    {
        var cutoff = DateTime.UtcNow.AddDays(-30);
        return await _context.TaskItems
            .Where(t => t.Status == Core.Enums.TaskStatus.Completed && t.UpdateHistory.Any(h => h.UserId == userId && h.UpdatedAt >= cutoff && h.NewValue == nameof(Core.Enums.TaskStatus.Completed)))
            .CountAsync();
    }

}
