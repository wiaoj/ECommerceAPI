using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse> {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenHandler tokenHandler) {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken) {
        ApplicationUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        user ??= await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        if(user is null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if(result.Succeeded) { //Authentication başarılı olmuş oluyor
            Token token = _tokenHandler.CreateAccessToken();
            return new LoginUserSuccessCommandResponse() {
                Token = token,
            };
        }

        //return new LoginUserErrorCommandResponse() {
        //    Message = "Username or password wrong"
        //};

        throw new AuthenticationErrorException();
    }
}