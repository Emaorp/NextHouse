using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Users.Queries.GetUsersList;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Users.Queries.GetUserById
{
    public sealed class GetUserByIdUseCase : IRequestHandler<GetUserByIdQuery, GetUserByIdResponseDTO>
    {
        private readonly IUsersRepository _context;

        public GetUserByIdUseCase(
            IUsersRepository context)
        {
            _context = context;
        }

        public async Task<GetUserByIdResponseDTO> Handle(
            GetUserByIdQuery request)
        {
            var user = await _context.GetByIdAsync(request.UserId);


            var userResponse = new GetUserByIdResponseDTO()
            {
                Id = user.Id,
                FirstName = user.FisrtName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId.ToString(),
                RoleName = _context.GetRoleByIdAsync(user.RoleId.ToString()).Result.Name
            };

            return userResponse;

        }
    }
}
