using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.GetRoleOptions
{
    public class GetRoleOptionsQuery : IRequest<IReadOnlyList<RoleOptionDTO>>
    {
    }
}