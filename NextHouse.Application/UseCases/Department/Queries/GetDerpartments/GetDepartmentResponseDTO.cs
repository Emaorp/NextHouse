using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Departament.Queries.GetDerpartaments
{
    public class GetDepartmentResponseDTO
    {
        public string Name { get; set; }

        public  Guid Id { get; set; }
    }
}
