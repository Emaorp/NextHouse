using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Security.Roles.Commands.UpdateRolePermissions;

public sealed class UpdateRolePermissionsCommand : IRequest<bool>
{
    public string RoleId { get; set; } = string.Empty;

    public List<string> PermissionIds { get; set; } = new();
}
