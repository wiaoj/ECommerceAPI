using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse> {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken) {
        ApplicationUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        user ??= await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if(user is null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if(result.Succeeded) { //Authentication başarılı olmuş oluyor
            //Yetkileri belirlememiz gerekiyor.
        }

        return new();
    }
}