﻿using ECommerceAPI.Application.Abstractions.Services;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.Roles.GetRoleById;
public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse> {
    private readonly IRoleService _roleService;

    public GetRoleByIdQueryHandler(IRoleService roleService) {
        _roleService = roleService;
    }

    public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken) {
        (String id, String name) = await _roleService.GetRoleById(request.Id);
        return new() {
            Id = id,
            Name = name
        };
    }
}