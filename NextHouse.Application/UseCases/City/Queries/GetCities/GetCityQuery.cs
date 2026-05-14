using NextHouse.Application.UseCases.Departament.Queries.GetDerpartaments;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.City.Queries.GetCities
{
    public class GetCityQuery : IRequest<List<GetCityResponseDTO>>
    {
        public Guid? DepartmentId { get; set; }
    }
}
