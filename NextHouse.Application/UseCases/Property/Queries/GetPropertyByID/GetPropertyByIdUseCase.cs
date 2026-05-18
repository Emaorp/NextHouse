using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.Queries.GetPropertyByID
{
    public class GetPropertyByIdUseCase : IRequestHandler<GetPropertyByIdQuery, GetPropertyByIdResponseDTO>
    {
        private readonly IPropertyRepository _propertiesRepository;

        public GetPropertyByIdUseCase(IPropertyRepository propertiesRepository)
        {
            _propertiesRepository = propertiesRepository;
        }

        public async Task<GetPropertyByIdResponseDTO> Handle(GetPropertyByIdQuery request)
        {
            var property = await _propertiesRepository.GetByIdWithImagesAsync(request.Id);

            if (property == null)
            {
                throw new BussinesRuleException("La propiedad no existe");
            }

            return new GetPropertyByIdResponseDTO
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                Price = property.Price,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                Area = property.Area,
                HasParking = property.HasParking,
                Address = property.Address,
                Neighborhood = property.Neighborhood,
                Type = (int)property.Type,
                City = property.City.Name,
                Department = property.City.Department.Name,
                ImageUrls = property.Images
                    .OrderByDescending(i => i.IsPrimary)
                    .Select(i => i.ImageUrl)
                    .ToList()
            };
        }
    }
}
