using NextHouse.Application.UseCases.Property.Queries.GetPropertyByID;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Queries.GetRequestByAgentId
{
    public class GetPropertyRequestByAgentIdQuery : IRequest<List<GetPropertyRequestByAgentIdResponseDTO>>
    {
        public string AgentId { get; set; }

    }
}
