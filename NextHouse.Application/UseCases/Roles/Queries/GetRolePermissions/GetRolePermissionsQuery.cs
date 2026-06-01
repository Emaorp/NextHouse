using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Queries.GetRolePermissions
{
    public sealed class GetRolePermissionsQuery
        : IRequest<List<GetRolePermissionsResponseDTO>>
    {
        public string RoleId { get; set; } = string.Empty;
    }
}
