using ECommerceAPI.Application.Abstractions.Services.Authentications;

namespace ECommerceAPI.Application.Abstractions.Services;
public interface IAuthService : IInternallAuthentication, IExternalAuthentication { }