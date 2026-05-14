using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Department.Queries.GetDerpartments;
using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Entities.Location;
using NextHouse.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Departament.Queries.GetDerpartaments
{
    public class GetDepartmentsUseCase: IRequestHandler<GetDepartmentsQuery, List<GetDepartmentResponseDTO>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentsUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<List<GetDepartmentResponseDTO>> Handle(GetDepartmentsQuery request)
        {

            IEnumerable<NextHouse.Domain.Entities.Location.Department> departments = await _departmentRepository.GetAllAsync();

            if (departments == null)
            {
                throw new BussinesRuleException("No existe ese departamento");
            }

            return departments.ToList().Select(d => new GetDepartmentResponseDTO
            {
                Id = d.Id,
               Name = d.Name,

    }).ToList();

        }
    }
}
