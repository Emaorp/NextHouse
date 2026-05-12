using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

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
            Domain.Entities.Properties.Property? property = await _propertiesRepository.GetByIdAsync(request.Id);

            if (property == null)
            {
                throw new BussinesRuleException("La sección no existe");
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
                Address = property.Address,
                Neighborhood = property.Neighborhood,
                Type = (int)property.Type,
                City = property.City.Name,
                Departament = property.City.Department.Name

            };

        }
    }

}
