using NextHouse.Domain.Entities.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property?> GetByIdAsync(Guid id);

        Task<IEnumerable<Property>> GetAllAsync();

        Task AddAsync(Property property);

        Task UpdateAsync(Property property);

        Task DeleteAsync(Property property);

        Task<IEnumerable<Property>> GetAllByCityAsync(Guid cityId);
    }
}
