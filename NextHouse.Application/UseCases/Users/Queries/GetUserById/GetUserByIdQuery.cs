using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQuery
    : IRequest<GetUserByIdResponseDTO>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
