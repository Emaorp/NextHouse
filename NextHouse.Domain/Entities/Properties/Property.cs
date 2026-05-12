using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Entities.Location;
using NextHouse.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Domain.Entities.Properties
{
    public class Property
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

        public PropertyType Type { get; set; }

        public PropertyStatus Status { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Dueño

        public Guid CityId { get; set; }
        public City City { get; set; } = null!;

        public string OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        // Agente asignado opcional
        public string? AgentId { get; set; }
        public User? Agent { get; set; }

        // Relaciones
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();

        public ICollection<PropertyRequest> Requests { get; set; } = new List<PropertyRequest>();
    }
}