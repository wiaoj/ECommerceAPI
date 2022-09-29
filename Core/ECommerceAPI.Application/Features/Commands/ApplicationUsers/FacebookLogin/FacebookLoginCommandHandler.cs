using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace ECommerceAPI.Application.Features.Commands.ApplicationUsers.FacebookLogin;
public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse> {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly HttpClient _httpClient;

    public FacebookLoginCommandHandler(UserManager<ApplicationUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory) {
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken) {
        String accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=3278540179047559&client_secret=8a143145aa24544aca028fecfcd50f56&grant_type=client_credentials", cancellationToken);

        FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        String userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse?.AccessToken}", cancellationToken);

        FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

        if(validation.Data.IsValid) {
            String userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);
            userInfo.Email ??= userInfo.Id; // kullanıcı izin vermiyorsa email paylaşılmıyor

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

            ApplicationUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            Boolean result = user is not null;

            // user null ise veritabanında aspnetuserlogins kısmı boştur ve kullanıcı kayıtlı değildir
            if(user is null) {
                user = await _userManager.FindByEmailAsync(userInfo.Email);
                if(user is null) {
                    user = new() {
                        Id = Guid.NewGuid().ToString(),
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        NameSurname = userInfo.Name
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if(result) {
                // AspNetUserLogins table
                await _userManager.AddLoginAsync(user, info); // ilgili tabloya geldiği dış kaynak özellikleri ile kaydediyoruz

                Token token = _tokenHandler.CreateAccessToken();
                return new() {
                    Token = token
                };
            }
        }
        throw new Exception("Invalid external authentication");
    }
}