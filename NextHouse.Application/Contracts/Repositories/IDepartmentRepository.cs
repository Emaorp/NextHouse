using NextHouse.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(Guid id);

        Task<IEnumerable<Department>> GetAllAsync();


    }
}
