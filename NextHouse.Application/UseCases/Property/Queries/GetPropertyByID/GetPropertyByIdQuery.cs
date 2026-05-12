using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Property.Queries.GetPropertyByID
{
    public class GetPropertyByIdQuery : IRequest<GetPropertyByIdResponseDTO>
    {
        public Guid Id { get; set; }
    }
}
