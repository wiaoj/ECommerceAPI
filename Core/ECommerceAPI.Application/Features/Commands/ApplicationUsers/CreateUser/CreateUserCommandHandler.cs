using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse> {
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService) {
        _userService = userService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken) {
        CreateUserResponse response = await _userService.CreateAsync(new() {
            NameSurname = request.NameSurname,
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            PasswordConfirm = request.PasswordConfirm,
        });

        return new() {
            Message = response.Message,
            Succeeded = response.Succeeded,
        };
    }
}