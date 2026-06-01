using NextHouse.Domain.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace NextHouse.Application.UseCases.Property.Commands.CreateProperty
{
    public class CreatePropertyDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public double Area { get; set; }

        public bool HasParking { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Neighborhood { get; set; } = string.Empty;

        public Guid CityId { get; set; }

        public PropertyType Type { get; set; }
        public string? OwnerId { get; set; }
        public string? AgentId { get; set; }

        /// <summary>
        /// URLs de las imágenes subidas (máximo 10)
        /// </summary>
        public List<string> ImageUrls { get; set; } = new();
    }
}
