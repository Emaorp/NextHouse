using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Roles.Queries
{
    public sealed class GetRolesResponseDTO
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
