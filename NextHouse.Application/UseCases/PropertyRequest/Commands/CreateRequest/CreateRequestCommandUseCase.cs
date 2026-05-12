using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Property.Commands.CreateProperty;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Commands.CreateRequest
{
    public class CreateRequestCommandUseCase : IRequestHandler<CreateRequestCommand, Guid>
    {
        private readonly IPropertyRequestRepository  _propertyRequestRepository;

        public CreateRequestCommandUseCase(IPropertyRequestRepository propertyRequestRepository)
        {
            _propertyRequestRepository = propertyRequestRepository;
        }

        public async Task<Guid> Handle(CreateRequestCommand request)
        {
            var dto = request.RequestDto;

            var newRequest = new Domain.Entities.Request.PropertyRequest
            {
                Id = Guid.NewGuid(),
                PropertyId = dto.PropertyId,
                ApplicationsName = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Message = dto.Message,
                Status = Domain.Entities.Request.RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow,
            };

            await _propertyRequestRepository.AddAsync(newRequest);

            return newRequest.Id;
        }

    }
}
