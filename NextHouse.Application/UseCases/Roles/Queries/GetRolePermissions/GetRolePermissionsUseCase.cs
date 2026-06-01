using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Queries.GetRolePermissions;

public sealed class GetRolePermissionsUseCase : IRequestHandler<GetRolePermissionsQuery, List<GetRolePermissionsResponseDTO>>
{
    private readonly IRoleRepository _context;

    public GetRolePermissionsUseCase(IRoleRepository context)
    {
        _context = context;
    }

    public async Task<List<GetRolePermissionsResponseDTO>> Handle(
        GetRolePermissionsQuery request)
    {

        var rolePermissions = await _context.GetRolePermissionsAsync(request.RoleId);

        var Permissions = await _context.GetPermissionsAsync();

        return Permissions.Select(permission =>
                new GetRolePermissionsResponseDTO
                {
                    PermissionId = permission.Id.ToString(),
                    Code = permission.Code,
                    Description = permission.Description,
                    Module = permission.Module,
                    Assigned = rolePermissions.Any(rp => rp.PermissionId == permission.Id)
                }).ToList();
    }
}