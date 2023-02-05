using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ECommerceAPI.API.Filters;

public class RolePermissionFilter : IAsyncActionFilter {
    private readonly IUserService _userService;

    public RolePermissionFilter(IUserService userService) {
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {

        String? userName = context.HttpContext.User.Identity?.Name;

        if(String.IsNullOrEmpty(userName) is false && userName is not "bertandeniz7@gmail.com") {
            ControllerActionDescriptor? descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            AuthorizeDefinitionAttribute? attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

            HttpMethodAttribute? httpMethodAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

            String code = $"{descriptor.ControllerName.Replace("Controller", String.Empty)}.{(httpMethodAttribute is null ? HttpMethods.Get : httpMethodAttribute.HttpMethods.First())}.{attribute.ActionType}.{attribute.Definition.Replace(" ", String.Empty)}".ToLowerInvariant();

            Boolean hasRole = await _userService.HasRolePermissionToEndpointAsync(userName, code);

            if(hasRole is false) {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();

        }
        await next();
    }
}