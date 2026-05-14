using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await _context.Departments
           .Where(x => x.Id == id)
           .FirstOrDefaultAsync();
        }
    }
}
