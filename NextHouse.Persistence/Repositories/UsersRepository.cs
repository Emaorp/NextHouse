using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Exceptions;
using NextHouse.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersRepository(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreateAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                Id = user.Id,
                FirstName = user.FisrtName,
                LastName = user.LastName,
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                RoleId = user.RoleId,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)
            {
                string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BussinesRuleException($"Error al crear el usuario: {errors}");
            }
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            ApplicationUser? appUser = await _userManager.FindByIdAsync(id);

            if (appUser is null)
            {
                throw new BussinesRuleException("El usuario no existe.");
            }

            IdentityResult result = await _userManager.DeleteAsync(appUser);

            if (!result.Succeeded)
            {
                string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BussinesRuleException($"Error al eliminar el usuario: {errors}");
            }
        }

        public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            ApplicationUser? appUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return User.Reconstitute(appUser.Id,
                                     appUser.FirstName,
                                     appUser.LastName,
                                     appUser.UserName,
                                     appUser.Email,
                                     appUser.EmailConfirmed,
                                     appUser.PhoneNumber,
                                     appUser.RoleId);
        }

       

        public async Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles.AsNoTracking()
                                       .OrderBy(r => r.Name)
                                       .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            ApplicationUser? appUser = await _userManager.FindByIdAsync(user.Id);

            if (appUser is null)
            {
                throw new BussinesRuleException("El usuario no existe.");
            }

            appUser.FirstName = user.FisrtName;
            appUser.LastName = user.LastName;
            appUser.Email = user.Email;
            appUser.UserName = user.Email;
            appUser.PhoneNumber = user.PhoneNumber;
            appUser.RoleId = user.RoleId;

            IdentityResult result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                string errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new BussinesRuleException($"Error al actualizar el usuario: {errors}");
            }
        }
        public async Task<List<User>> GetByRoleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.Role.Name == roleName)
                .ToListAsync(cancellationToken);

            return users.Select(u => User.Reconstitute(
                u.Id,
                u.FirstName,
                u.LastName,
                u.UserName,
                u.Email,
                u.EmailConfirmed,
                u.PhoneNumber,
                u.RoleId
            )).ToList();
        }
    }
}
