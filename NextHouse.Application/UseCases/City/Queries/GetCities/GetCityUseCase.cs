using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Departament.Queries.GetDerpartaments;
using NextHouse.Application.UseCases.Department.Queries.GetDerpartments;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.City.Queries.GetCities
{
    public class GetCityUseCase : IRequestHandler<GetCityQuery, List<GetCityResponseDTO>>
    {
        private readonly ICityRepository _cityRepository;

        public GetCityUseCase(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public async Task<List<GetCityResponseDTO>> Handle(GetCityQuery request)
        {

            IEnumerable<NextHouse.Domain.Entities.Location.City> cities = await _cityRepository.GetByDepartmentIdAsync(request.DepartmentId.Value);

            if (cities == null)
            {
                throw new BussinesRuleException("No existe esa ciudad");
            }

            return cities.ToList().Select(d => new GetCityResponseDTO
            {
                Id = d.Id,
                Name = d.Name,

            }).ToList();

        }
  
    }
}
