namespace TaskManager.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
