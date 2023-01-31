using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.DTOs.Configuration;
using ECommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using Action = ECommerceAPI.Application.DTOs.Configuration.Action;

namespace ECommerceAPI.Infrastructure.Services.Configurations;
public class ApplicationService : IApplicationService {
    public List<Menu> GetAuthorizeDefinitionEndpoints(Type type) {
        Assembly assembly = Assembly.GetAssembly(type);
        IEnumerable<Type> controllers = assembly.GetTypes().Where(type => type.IsAssignableTo(typeof(ControllerBase)));

        List<Menu> menus = new();
        if(controllers is not null) {
            foreach(Type controller in controllers) {
                IEnumerable<MethodInfo>? actions = controller.GetMethods().Where(x => x.IsDefined(typeof(AuthorizeDefinitionAttribute)));

                if(actions is not null) {
                    foreach(MethodInfo action in actions) {
                        var attributes = action.GetCustomAttributes(true);

                        if(attributes is not null) {
                            Menu menu = null;
                            AuthorizeDefinitionAttribute? authorizeDefinitionAttribute = attributes.FirstOrDefault(x => x.GetType().Equals(typeof(AuthorizeDefinitionAttribute))) as AuthorizeDefinitionAttribute;

                            if(menus.Any(m => m.Name.Equals(authorizeDefinitionAttribute?.Menu)) is false) {
                                menu = new() {
                                    Name = authorizeDefinitionAttribute.Menu
                                };
                                menus.Add(menu);
                            } else {
                                menu = menus.FirstOrDefault(m => m.Name.Equals(authorizeDefinitionAttribute.Menu));
                            }

                            Action _action = new() {
                                ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                Definition = authorizeDefinitionAttribute.Definition,
                            };

                            HttpMethodAttribute? httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                            _action.HttpType = httpAttribute is null
                                ? HttpMethods.Get
                                : httpAttribute.HttpMethods.First();

                            _action.Code = $"{controller.Name.Replace("Controller", String.Empty)}.{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", String.Empty)}".ToLowerInvariant();

                            menu.Actions.Add(_action);

                        }
                    }
                }
            }
        }

        return menus;
    }
}