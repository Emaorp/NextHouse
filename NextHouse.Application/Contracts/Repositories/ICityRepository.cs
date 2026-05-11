using NextHouse.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface ICityRepository
    {
        Task<City?> GetByIdAsync(Guid id);

        Task<IEnumerable<City>> GetAllAsync();

        Task<IEnumerable<City>> GetByDepartmentIdAsync(Guid departmentId);

        Task<City?> GetWithPropertiesAsync(Guid id);

        Task AddAsync(City city);

        Task UpdateAsync(City city);

        Task DeleteAsync(City city);

        Task<bool> ExistsAsync(Guid id);

        Task SaveChangesAsync();
    }
}
