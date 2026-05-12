using NextHouse.Application.Utilites.Mediator;
using System;

namespace NextHouse.Application.UseCases.Property.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<Guid>
    {
        public CreatePropertyDto PropertyDto { get; set; }

        public CreatePropertyCommand(CreatePropertyDto propertyDto)
        {
            PropertyDto = propertyDto;
        }
    }
}