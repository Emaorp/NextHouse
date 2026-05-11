
namespace NextHouse.Domain.Entities.Location
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NextHouse.Domain.Entities.Property;
    public class City
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid DepartmentId { get; set; }

        public Department Department { get; set; } = null!;

        // Relaciones
        public ICollection<Property> Properties { get; set; }
            = new List<Property>();
    }
}
