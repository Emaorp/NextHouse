using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Queries
{
    public sealed class GetRolesUseCase :
     IRequestHandler<GetRolesQuery, List<GetRolesResponseDTO>>
    {
        private readonly IRoleRepository _context;

        public GetRolesUseCase(IRoleRepository context)
        {
            _context = context;
        }

        public async Task<List<GetRolesResponseDTO>> Handle(
            GetRolesQuery request)
        {
            var roles = await _context.GetRolesAsync();

            return roles
                .Select(role => new GetRolesResponseDTO
                {
                    Id = role.Id.ToString(),
                    Name = role.Name
                })
                .OrderBy(role => role.Name)
                .ToList();
        }
    }
}