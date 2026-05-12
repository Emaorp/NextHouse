using MediatR;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.CreateProperty
{
    public class CreatePropertyCommandHandler : global::MediatR.IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyRepository _propertyRepository;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Guid> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var dto = request.PropertyDto;

            var newProperty = new NextHouse.Domain.Entities.Property.Property
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Bedrooms = dto.Bedrooms,
                Bathrooms = dto.Bathrooms,
                Area = dto.Area,
                Address = dto.Address,
                Neighborhood = dto.Neighborhood,
                CityId = dto.CityId,
                Type = dto.Type,
                CreatedAt = DateTime.Now,
                OwnerId = "00000000-0000-0000-0000-000000000000"
            };

            await _propertyRepository.AddAsync(newProperty);

            return newProperty.Id;
        }
    }
}