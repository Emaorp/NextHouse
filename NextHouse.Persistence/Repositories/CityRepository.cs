using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities
                .ToListAsync();
        }

        public async Task<IEnumerable<City>> GetByDepartmentIdAsync(Guid departmentId)
        {
            return await _context.Cities
               .Where(x=> x.DepartmentId == departmentId)
               .ToListAsync();
        }

        public async Task<City?> GetByIdAsync(Guid id)
        {
            return await _context.Cities
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();
        }

        public async Task<City?> GetWithPropertiesAsync(Guid id)
        {
            return await _context.Cities
               .Include(x => x.Properties)
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();
        }
    }
}