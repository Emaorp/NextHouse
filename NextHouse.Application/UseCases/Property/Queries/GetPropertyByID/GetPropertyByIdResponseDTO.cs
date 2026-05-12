using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Entities.Location;
using NextHouse.Domain.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Property.Queries.GetPropertyByID
{
    public class GetPropertyByIdResponseDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public double Area { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Neighborhood { get; set; } = string.Empty;

        public string Departament { get; set; } = string.Empty;

        public int Type { get; set; }

        public string City { get; set; } = null!; 


    }
}
