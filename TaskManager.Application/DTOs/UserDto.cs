
namespace TaskManager.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;

    public static explicit operator UserDto(User user)
        => new() { Id = user.Id, Username = user.Username };
}
