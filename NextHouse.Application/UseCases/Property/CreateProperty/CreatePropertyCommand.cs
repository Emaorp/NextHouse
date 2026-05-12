using MediatR;
using NextHouse.Application.Utilites.Mediator;
using System;

namespace NextHouse.Application.UseCases.Property.CreateProperty
{
    public class CreatePropertyCommand : MediatR.IRequest<Guid>
    {
        public CreatePropertyDto PropertyDto { get; set; }

        public CreatePropertyCommand(CreatePropertyDto propertyDto)
        {
            PropertyDto = propertyDto;
        }
    }
}