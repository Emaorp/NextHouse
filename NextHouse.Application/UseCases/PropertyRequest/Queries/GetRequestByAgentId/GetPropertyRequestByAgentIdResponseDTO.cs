using NextHouse.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Queries.GetRequestByAgentId
{
    public class GetPropertyRequestByAgentIdResponseDTO
    {

        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }

        public string PropertyTitle { get; set; } = string.Empty;

        public RequestStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
