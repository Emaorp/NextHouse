using NextHouse.Domain.Entities.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Property.CreateProperty
{
    public class CreatePropertyDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public double Area { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Neighborhood { get; set; } = string.Empty;

        public Guid CityId { get; set; }

        public PropertyType Type { get; set; }
    }
}
