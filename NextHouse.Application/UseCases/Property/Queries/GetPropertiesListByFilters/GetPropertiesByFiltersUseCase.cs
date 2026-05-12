using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;


namespace NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters
{
    public class GetPropertiesByFiltersUseCase
    : IRequestHandler<
        GetPropertiesByFiltersQuery,
        List<PropertyFilterResponseDTO>>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertiesByFiltersUseCase(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        } 
        public async Task<List<PropertyFilterResponseDTO>> Handle(GetPropertiesByFiltersQuery request)
        {

            IEnumerable<Domain.Entities.Properties.Property> properties = await _propertyRepository.GetByFiltersAsync(request);

            if (properties == null)
            {
                throw new BussinesRuleException("No existe esa propiedad");
            }

            return properties.ToList().Select(p => new PropertyFilterResponseDTO
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                City = p.City.Name,
                Department = p.City.Department.Name,
                Status = p.Status

            }).ToList();

        }
    }
}
