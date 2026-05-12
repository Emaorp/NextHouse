
namespace NextHouse.Domain.Entities.Request
{
    using NextHouse.Domain.Entities.Account;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NextHouse.Domain.Entities.Properties;
    public class PropertyRequest
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }

        public Property Property { get; set; } = null!;

        public string ApplicationsName { get; set; }

        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }

        public RequestStatus Status { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
