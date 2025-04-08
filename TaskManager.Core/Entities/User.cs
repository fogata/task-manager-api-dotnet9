namespace TaskManager.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
