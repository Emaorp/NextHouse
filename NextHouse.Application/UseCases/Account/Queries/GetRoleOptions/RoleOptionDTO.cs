using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.GetRoleOptions
{
    public class RoleOptionDTO
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}