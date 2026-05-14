using Microsoft.AspNetCore.Mvc.Rendering;
using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;

namespace NextHouse.Web.DTOs.Properties
{
    public class PropertyFilterViewModel
    {
        public Guid? DepartmentId { get; set; }

        public Guid? CityId { get; set; }

        public int? PropertyType { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public int? Bedrooms { get; set; }

        public int? Bathrooms { get; set; }

        public IEnumerable<SelectListItem>? Departments { get; set; }

        public IEnumerable<SelectListItem>? Cities { get; set; }

        public IEnumerable<PropertyFilterResponseDTO>? Properties { get; set; }
    }
}
