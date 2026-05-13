using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.UserHasPermission
{
    public class UserHasPermissionQuery : IRequest<bool>
    {
        public required string UserId { get; set; }
        public required string PermissionCode { get; set; }
    }
}
