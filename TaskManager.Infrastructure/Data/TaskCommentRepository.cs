using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Data;

public class TaskCommentRepository : ITaskCommentRepository
{
    private readonly AppDbContext _context;

    public TaskCommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskComment comment)
    {
        await _context.TaskComments.AddAsync(comment);
    }

    public async Task<IEnumerable<TaskComment>> GetByTaskIdAsync(Guid taskId)
    {
        return await _context.TaskComments
            .Where(c => c.TaskItemId == taskId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}

