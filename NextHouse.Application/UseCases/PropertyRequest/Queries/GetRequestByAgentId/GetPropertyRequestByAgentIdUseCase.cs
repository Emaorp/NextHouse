using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Property.Queries.GetPropertyByID;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Queries.GetRequestByAgentId
{
    public class GetPropertyRequestByAgentIdUseCase : IRequestHandler<GetPropertyRequestByAgentIdQuery, List<GetPropertyRequestByAgentIdResponseDTO>>
    {
        private readonly IPropertyRequestRepository _propertyRequestsRepository;

        public GetPropertyRequestByAgentIdUseCase(IPropertyRequestRepository propertyRequestsRepository)
        {
            _propertyRequestsRepository = propertyRequestsRepository;
        }
        public async Task< List<GetPropertyRequestByAgentIdResponseDTO>> Handle(GetPropertyRequestByAgentIdQuery request)
        {
            var requests =
                await _propertyRequestsRepository
                    .GetByAgentIdAsync(request.AgentId);

            return requests
                .Select(x =>
                    new GetPropertyRequestByAgentIdResponseDTO
                    {
                        Id = x.Id,
                        PropertyId = x.PropertyId,
                        PropertyTitle = x.Property.Title,
                        ApplicationsName = x.ApplicationsName,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        Message = x.Message,
                        Status = x.Status,
                        CreatedAt = x.CreatedAt
                    })
                .ToList();
        }


    }
}
