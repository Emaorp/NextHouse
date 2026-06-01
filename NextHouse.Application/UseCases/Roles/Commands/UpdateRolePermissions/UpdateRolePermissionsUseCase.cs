using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Security.Roles.Commands.UpdateRolePermissions;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Commands.UpdateRolePermissions
{
    public sealed class UpdateRolePermissionsUseCase :
    IRequestHandler<UpdateRolePermissionsCommand, bool>
    {
        private readonly IRoleRepository _context;

        public UpdateRolePermissionsUseCase(
            IRoleRepository context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            UpdateRolePermissionsCommand request)
        {

            List<RolePermission> currentPermissions =
                await _context.GetRolePermissionsAsync(request.RoleId);

            await _context.DeleteRolePermissions(
                currentPermissions);

            if (request.PermissionIds.Any())
            {
                List<RolePermission> newPermissions =
                    request.PermissionIds
                        .Distinct()
                        .Select(permissionId =>
                            new RolePermission(
                               Guid.Parse(request.RoleId),
                                Guid.Parse(permissionId))).ToList();

                await _context.AddRolePermissions(
                        newPermissions);
            }

            return true;
        }

       
    }
}