using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Users.Queries.GetUsersList
{
    public sealed class GetUsersUseCase : IRequestHandler<GetUserQuery, List<GetUserResponseDTO>>
    {
        private readonly IUsersRepository _context;

        public GetUsersUseCase(
            IUsersRepository context)
        {
            _context = context;
        }

        public async Task<List<GetUserResponseDTO>> Handle(
            GetUserQuery request )
        {
            var user = await _context.GetUsersList();


            return user.Select(u => new GetUserResponseDTO
            {
                Id = u.Id,
                FirstName = u.FisrtName,
                LastName = u.LastName,
                Email = u.Email,
                RoleId = u.RoleId.ToString(),
                RoleName = _context.GetRoleByIdAsync(u.RoleId.ToString()).Result.Name
            }).ToList();
        }
    }
}
