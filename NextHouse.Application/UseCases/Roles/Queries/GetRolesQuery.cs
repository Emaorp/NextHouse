using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Queries
{
    public sealed class GetRolesQuery : IRequest<List<GetRolesResponseDTO>>
    {
    }
}
