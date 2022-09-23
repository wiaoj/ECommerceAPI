using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.CreateUser;

public class CreateUserCommandRequestHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse> {
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateUserCommandRequestHandler(UserManager<ApplicationUser> userManager) {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken) {
        IdentityResult result = await _userManager.CreateAsync(new() {
            Id = Guid.NewGuid().ToString(),
            NameSurname = request.NameSurname,
            UserName = request.UserName,
            Email = request.Email,
        }, request.Password);

        CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

        if(result.Succeeded)
            response.Message = "Success :)";
        else
            foreach(var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}";

        return response;
        //throw new UserCreateFailedException();
    }
}