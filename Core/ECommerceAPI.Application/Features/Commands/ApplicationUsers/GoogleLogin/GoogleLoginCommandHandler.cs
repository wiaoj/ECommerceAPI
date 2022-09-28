using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.GoogleLogin;
public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse> {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenHandler _tokenHandler;

    public GoogleLoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenHandler tokenHandler) {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken) {

        var settings = new GoogleJsonWebSignature.ValidationSettings() {
            Audience = new List<String> { "544170587397-irgcfffm987ckuguk3b7vrt450oe9b15.apps.googleusercontent.com" } // Client Id değeri client'te kullandığımız id ile aynı id'yi kullanıyoruz ( ikisi aynı projeye bağlıdır )
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

        var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

        ApplicationUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        Boolean result = user is not null;

        // user null ise veritabanında aspnetuserlogins kısmı boştur ve kullanıcı kayıtlı değildir
        if(user is null) {
            user = await _userManager.FindByEmailAsync(payload.Email);
            if(user is null) {
                user = new() {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.Email,
                    NameSurname = payload.Name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if(result)
            // AspNetUserLogins table
            await _userManager.AddLoginAsync(user, info); // ilgili tabloya geldiği dış kaynak özellikleri ile kaydediyoruz
        else
            throw new Exception("Invalid external authentication");

        Token token = _tokenHandler.CreateAccessToken();

        return new() {
            Token = token
        };
    }
}