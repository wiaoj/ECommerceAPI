using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Exceptions;
using MediatR;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.UpdatePassword;
public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse> {
    private readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService) {
        _userService = userService;
    }

    public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken) {

        if(request.Password.Equals(request.PasswordConfirm) is false) {
            throw new PasswordChangeFailedException("Şifreyi doğrulayınız");
        }

        await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);

        return new();
    }
}