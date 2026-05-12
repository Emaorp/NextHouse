using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Domain.Entities.Properties
{
    public class PropertyImage
    {
        public Guid Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsPrimary { get; set; }

        public Guid PropertyId { get; set; }

        public Property Property { get; set; } = null!;
    }
}
