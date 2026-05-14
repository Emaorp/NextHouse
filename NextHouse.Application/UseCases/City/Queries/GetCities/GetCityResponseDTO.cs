using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.City.Queries.GetCities
{
    public class GetCityResponseDTO
    {
        public string Name { get; set; }

        public Guid Id { get; set; }
    }
}
