using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Entities.Properties;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandUseCase : IRequestHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;

        public CreatePropertyCommandUseCase(
            IPropertyRepository propertyRepository,
            IPropertyImageRepository propertyImageRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
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
                HasParking = dto.HasParking,
                Address = dto.Address,
                Neighborhood = dto.Neighborhood,
                CityId = dto.CityId,
                Type = dto.Type,
                Status = Domain.Entities.Properties.PropertyStatus.Available,
                CreatedAt = DateTime.Now,
                OwnerId = dto.OwnerId
            };

            await _propertyRepository.AddAsync(newProperty);

            // Guardar imágenes (máximo 10)
            var imageUrls = dto.ImageUrls
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .Take(10)
                .ToList();

            if (imageUrls.Any())
            {
                var images = imageUrls.Select((url, index) => new PropertyImage
                {
                    Id = Guid.NewGuid(),
                    PropertyId = newProperty.Id,
                    ImageUrl = url,
                    IsPrimary = index == 0
                }).ToList();

                await _propertyImageRepository.AddRangeAsync(images);
            }

            return newProperty.Id;
        }
    }
}
