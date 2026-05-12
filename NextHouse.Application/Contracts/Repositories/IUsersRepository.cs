using NextHouse.Domain.Entities.Account;
using NextHouse.Application.UseCases.Users.Queries.GetUsersList;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IUsersRepository
    {

        Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

        Task CreateAsync(User user, string password, CancellationToken cancellationToken = default);

        Task UpdateAsync(User user, CancellationToken cancellationToken = default);

        Task DeleteAsync(string id, CancellationToken cancellationToken = default);

        Task<List<Role>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}
