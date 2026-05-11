using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.PropertyRequest.Create
{
    public class CreatePropertyRequestDto
    {
        public Guid PropertyId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
