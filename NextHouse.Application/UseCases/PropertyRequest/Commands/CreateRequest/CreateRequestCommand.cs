using NextHouse.Application.UseCases.Property.Commands.CreateProperty;
using NextHouse.Application.UseCases.PropertyRequest.Commands.Create;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Commands.CreateRequest
{
    public class CreateRequestCommand : IRequest<Guid>
    {
        public CreateRequestDto RequestDto { get; set; }

        public CreateRequestCommand(CreateRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    

    }
}
