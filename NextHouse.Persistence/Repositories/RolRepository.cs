using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Repositories
{
    public sealed class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        
       

        public async Task UpdatePermissionsAsync(
            string roleId,
            List<string> permissionIds)
        {
            List<RolePermission> currentPermissions =

                await _context.RolePermissions
                    .Where(x => x.RoleId == Guid.Parse(roleId))
                    .ToListAsync();

            _context.RolePermissions.RemoveRange(
                currentPermissions);

            if (permissionIds.Any())
            {
                List<RolePermission> newPermissions =
                    permissionIds
                        .Distinct()
                        .Select(permissionId =>
                            new RolePermission(Guid.Parse(roleId), Guid.Parse(permissionId)))
                        .ToList();

                await _context.RolePermissions
                    .AddRangeAsync(newPermissions);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles.AsNoTracking()
                                       .OrderBy(r => r.Name)
                                       .ToListAsync(cancellationToken);
        }



        public async Task<List<RolePermission>> GetRolePermissionsAsync(string roleId)
        {
            return await _context.RolePermissions
                .Where(x => x.RoleId == Guid.Parse(roleId))
                .ToListAsync();
        }
        public async Task<List<Permission>> GetPermissionsAsync()
        {
            return await _context.Permissions
                .ToListAsync();
        }
        public async Task AddRolePermissions(List<RolePermission> rolePermissions)
        {
            await _context.RolePermissions.AddRangeAsync(rolePermissions);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRolePermissions(List<RolePermission> rolePermissions)
        {
             _context.RolePermissions.RemoveRange(rolePermissions);
            await _context.SaveChangesAsync();
        }
    }
}