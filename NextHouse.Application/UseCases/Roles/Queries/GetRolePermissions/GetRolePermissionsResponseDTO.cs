using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Queries.GetRolePermissions
{

    public sealed class GetRolePermissionsResponseDTO
    {
        public string PermissionId { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Module { get; set; } = string.Empty;

        public bool Assigned { get; set; }

    }
}
