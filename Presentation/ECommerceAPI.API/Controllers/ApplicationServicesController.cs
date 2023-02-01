using ECommerceAPI.Application.Abstractions.Services.Configurations;
using ECommerceAPI.Application.Consts;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Application.Enums;
using ECommerceAPI.Infrastructure.Services.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class ApplicationServicesController : ControllerBase {
    private readonly IApplicationService _applicationService;

    public ApplicationServicesController(IApplicationService applicationService) {
        _applicationService = applicationService;
    }

    [HttpGet]
    [AuthorizeDefinition(
        Menu = AuthorizeDefinitionConstants.ApplicationServices,
        ActionType = ActionType.Reading,
        Definition = "Get Authorize Definition Endpoints"
        )]
    public IActionResult GetAuthorizeDefinitionEndpoints() {
        return Ok(_applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program)));
    }
}