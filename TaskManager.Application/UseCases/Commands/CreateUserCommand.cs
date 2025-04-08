using TaskManager.Application.DTOs;

namespace TaskManager.Application.UseCases.Commands;

public class CreateUserCommand
{
    public CreateUserDto Dto { get; set; }

    public CreateUserCommand(CreateUserDto dto)
    {
        Dto = dto;
    }
}
