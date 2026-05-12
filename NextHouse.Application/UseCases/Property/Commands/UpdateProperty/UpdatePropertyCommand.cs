using NextHouse.Application.Utilites.Mediator;
using System;

namespace NextHouse.Application.UseCases.Property.Commands.UpdateProperty
{
   public class UpdatePropertyCommand : IRequest<bool>
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
    }
}