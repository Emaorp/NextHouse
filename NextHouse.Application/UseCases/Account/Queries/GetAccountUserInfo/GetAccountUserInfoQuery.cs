using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.GetAccountUserInfo
{
    public class GetAccountUserInfoQuery : IRequest<UserAccountInfoDTO>
    {
        public required string UserId { get; set; }
    }
}
