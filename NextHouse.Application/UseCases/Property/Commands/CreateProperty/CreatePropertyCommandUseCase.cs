using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandUseCase : IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyRepository _propertyRepository;

        public CreatePropertyCommandUseCase(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Guid> Handle(CreatePropertyCommand request)
        {
            var dto = request.PropertyDto;

            var newProperty = new Domain.Entities.Properties.Property
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
                OwnerId = "246A3F84-4376-441C-B410-15F647ABDA4E"
            };

            await _propertyRepository.AddAsync(newProperty);

            return newProperty.Id;
        }

    }
}