using Microsoft.AspNetCore.Identity;
using NextHouse.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
