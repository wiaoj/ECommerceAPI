using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Application.Exceptions;
using ECommerceAPI.Application.Features.Commands.ApplicationUsers.LoginUser;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ECommerceAPI.Persistence.Services;
public class AuthService : IAuthService {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;

    public AuthService(IHttpClientFactory httpClientFactory,
                       IConfiguration configuration,
                       UserManager<ApplicationUser> userManager,
                       ITokenHandler tokenHandler,
                       SignInManager<ApplicationUser> signInManager,
                       IUserService userService) {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _userManager = userManager;
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
        _userService = userService;
    }

    private async Task<Token> CreateUserExternalAsync(ApplicationUser user, String email, String name, UserLoginInfo info) {
        Boolean result = user is not null;

        // user null ise veritabanında aspnetuserlogins kısmı boştur ve kullanıcı kayıtlı değildir
        if(user is null) {
            user = await _userManager.FindByEmailAsync(email);
            if(user is null) {
                user = new() {
                    Id = Guid.NewGuid().ToString(),
                    Email = email,
                    UserName = email,
                    NameSurname = name
                };
                var identityResult = await _userManager.CreateAsync(user);
                result = identityResult.Succeeded;
            }
        }

        if(result) {
            // AspNetUserLogins table
            await _userManager.AddLoginAsync(user, info); // ilgili tabloya geldiği dış kaynak özellikleri ile kaydediyoruz

            Token token = _tokenHandler.CreateAccessToken(user);
            await _userService.UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 60 * 60);
            return token;
        }

        throw new Exception("Invalid external authentication");
    }

    public async Task<Token> FacebookLoginAsync(String authToken) {
        String accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

        FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

        String userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

        FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

        if(validation?.Data.IsValid is not null) {
            String userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

            FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);
            //userInfo.Email ??= userInfo.Id; // kullanıcı izin vermiyorsa email paylaşılmıyor

            var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

            ApplicationUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, userInfo?.Email, userInfo?.Name, info);
        }
        throw new Exception("Invalid external authentication");
    }

    public async Task<Token> GoogleLoginAsync(String idToken) {
        var settings = new GoogleJsonWebSignature.ValidationSettings() {
            Audience = new List<String> { _configuration["ExternalLoginSettings:Google:Client_ID"] } // Client Id değeri client'te kullandığımız id ile aynı id'yi kullanıyoruz ( ikisi aynı projeye bağlıdır )
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

        ApplicationUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

        return await CreateUserExternalAsync(user, payload.Email, payload.Name, info);
    }

    public async Task<Token> LoginAsync(String userNameOrEmail, String password) {
        ApplicationUser user = await _userManager.FindByNameAsync(userNameOrEmail)
            ?? await _userManager.FindByEmailAsync(userNameOrEmail)
            ?? throw new NotFoundUserException();

        //if(user is null)
        //    throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if(result.Succeeded) { //Authentication başarılı olmuş oluyor
            Token token = _tokenHandler.CreateAccessToken(user);
            await _userService.UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 15);
            return token;
        }

        //return new LoginUserErrorCommandResponse() {
        //    Message = "Username or password wrong"
        //};
        throw new AuthenticationErrorException();
    }

    public async Task<Token> RefreshTokenLoginAsync(String refreshToken) {
        ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken.Equals(refreshToken));

        if(user is not null && user.RefreshTokenEndDate > DateTime.UtcNow) {
            Token token = _tokenHandler.CreateAccessToken(user);
            await _userService.UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 60 * 60);
            return token;
        }

        throw new NotFoundUserException();
    }
}