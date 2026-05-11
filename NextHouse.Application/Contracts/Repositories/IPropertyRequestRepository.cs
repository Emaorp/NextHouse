using NextHouse.Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IPropertyRequestRepository
    {
        Task<PropertyRequest?> GetByIdAsync(Guid id);

        Task<IEnumerable<PropertyRequest>> GetAllAsync();

        Task<IEnumerable<PropertyRequest>> GetByTenantIdAsync(Guid tenantId);

        Task<IEnumerable<PropertyRequest>> GetByPropertyIdAsync(Guid propertyId);

        Task AddAsync(PropertyRequest request);

        Task UpdateAsync(PropertyRequest request);

        Task DeleteAsync(PropertyRequest request);

        Task<bool> ExistsAsync(Guid id);

        Task SaveChangesAsync();
    }
}
