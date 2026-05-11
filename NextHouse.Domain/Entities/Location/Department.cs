using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace NextHouse.Domain.Entities.Location
{
    public class Department
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid CountryId { get; set; }
        // Relaciones
        public ICollection<City> Cities { get; set; }
            = new List<City>();
    }
}
