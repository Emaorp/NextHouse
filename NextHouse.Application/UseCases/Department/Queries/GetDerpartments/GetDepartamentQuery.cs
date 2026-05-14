using NextHouse.Application.UseCases.Departament.Queries.GetDerpartaments;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Department.Queries.GetDerpartments
{

    public class GetDepartmentsQuery
        : IRequest<List<GetDepartmentResponseDTO>>
    {
    }
}
