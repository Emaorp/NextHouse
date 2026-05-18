using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;
using NextHouse.Domain.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtiene una propiedad por ID incluyendo sus imágenes
        /// </summary>
        Task<Property?> GetByIdWithImagesAsync(Guid id);

        Task<IEnumerable<Property>> GetAllAsync();

        Task AddAsync(Property property);

        Task UpdateAsync(Property property);

        Task DeleteAsync(Property property);

        Task<IEnumerable<Property>> GetAllByCityAsync(Guid cityId);

        Task<IEnumerable<Property>> GetByFiltersAsync(GetPropertiesByFiltersQuery filters);
    }
}
