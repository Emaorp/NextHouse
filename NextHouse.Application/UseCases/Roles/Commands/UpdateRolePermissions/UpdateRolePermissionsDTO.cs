using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Commands.UpdateRolePermissions
{
    public sealed class UpdateRolePermissionsDTO
    {
        public string RoleId { get; set; } = string.Empty;

        public List<string> PermissionIds { get; set; } = new();
    }
}
