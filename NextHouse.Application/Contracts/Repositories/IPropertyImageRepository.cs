using NextHouse.Domain.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IPropertyImageRepository
    {
        Task<PropertyImage?> GetByIdAsync(Guid id);

        Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(Guid propertyId);

        Task<PropertyImage?> GetPrimaryImageAsync(Guid propertyId);

        Task AddAsync(PropertyImage image);

        Task AddRangeAsync(IEnumerable<PropertyImage> images);

        Task UpdateAsync(PropertyImage image);

        Task DeleteAsync(PropertyImage image);

        Task DeleteRangeAsync(IEnumerable<PropertyImage> images);

        Task<bool> ExistsAsync(Guid id);

        Task SaveChangesAsync();
    }
}
