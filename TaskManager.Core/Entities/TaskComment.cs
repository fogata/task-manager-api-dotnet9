namespace TaskManager.Core.Entities;

public class TaskComment
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid TaskItemId { get; set; }
}
