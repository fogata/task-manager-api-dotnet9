using TaskManager.Application.DTOs;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class CreateUserHandler
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto> HandleAsync(CreateUserCommand command)
    {
        var user = (User)command.Dto;
        await _repository.AddAsync(user);
        return (UserDto)user;
    }
}
