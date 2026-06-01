using NextHouse.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IRoleRepository
    {
         Task UpdatePermissionsAsync(
            string roleId,
            List<string> permissionIds);
        Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken = default);

        Task<List<RolePermission>> GetRolePermissionsAsync(string roleId);

        Task AddRolePermissions(List<RolePermission> rolePermissions);

        Task DeleteRolePermissions(List<RolePermission> rolePermissions);

        Task<List<Permission>> GetPermissionsAsync();
    }
}
