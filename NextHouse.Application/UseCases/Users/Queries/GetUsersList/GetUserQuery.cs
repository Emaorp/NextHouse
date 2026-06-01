using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Users.Queries.GetUsersList
{
    public sealed class GetUserQuery
    : IRequest<List<GetUserResponseDTO>>
    {
    }
}
