﻿using ECommerceAPI.Application.Abstractions.Services.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicationServicesController : ControllerBase {
    private readonly IApplicationService _applicationService;

    public ApplicationServicesController(IApplicationService applicationService) {
        _applicationService = applicationService;
    }

    [HttpGet]
    public IActionResult GetAuthorizeDefinitionEndpoints() {
        return Ok(_applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program)));
    }
}