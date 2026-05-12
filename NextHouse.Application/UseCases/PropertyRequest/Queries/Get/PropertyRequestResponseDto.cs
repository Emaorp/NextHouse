using NextHouse.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Querys.Get
{
    public class PropertyRequestResponseDto
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }

        public string PropertyTitle { get; set; } = string.Empty;

        public Guid TenantId { get; set; }

        public string TenantName { get; set; } = string.Empty;

        public RequestStatus Status { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
