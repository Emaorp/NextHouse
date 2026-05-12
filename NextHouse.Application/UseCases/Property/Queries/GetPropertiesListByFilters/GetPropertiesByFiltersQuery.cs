using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters
{
    public class GetPropertiesByFiltersQuery
    : IRequest<List<PropertyFilterResponseDTO>>
    {
        public Guid? CityId { get; set; }
        public string? PropertyType { get; set; } = string.Empty;

        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public int? Bedrooms { get; set; }

        public int? Bathrooms { get; set; } 
        
    }

}
