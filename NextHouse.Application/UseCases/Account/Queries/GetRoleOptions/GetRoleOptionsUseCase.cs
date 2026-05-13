using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.GetRoleOptions
{
    public sealed class GetRoleOptionsUseCase : IRequestHandler<GetRoleOptionsQuery, IReadOnlyList<RoleOptionDTO>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetRoleOptionsUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IReadOnlyList<RoleOptionDTO>> Handle(GetRoleOptionsQuery query)
        {
            List<Role> roles = await _usersRepository.GetRolesAsync();

            return roles.Select(r => new RoleOptionDTO
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();
        }
    }
}