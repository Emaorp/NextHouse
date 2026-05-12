using NextHouse.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Commands.Create
{
    public class CreateRequestDto
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public Guid PropertyId  { get; set; }

    }
}
