namespace TaskManager.Application.DTOs;

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;

    public static explicit operator User(CreateUserDto dto)
        => new() { Username = dto.Username };
}
