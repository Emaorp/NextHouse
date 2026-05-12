using NextHouse.Domain.Entities.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Property.Queries.GetProperty
{
    public class PropertyResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string City { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public PropertyStatus Status { get; set; }
    }
}
