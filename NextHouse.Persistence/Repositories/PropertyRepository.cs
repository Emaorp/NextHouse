
using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Property;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Persistence.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;

        public PropertyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(Guid id)
        {
            return await _context.Properties
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Property property)
        {
            _context.Properties.Remove(property);

            await Task.CompletedTask;
        }
        public async Task<IEnumerable<Property>> GetAllByCityAsync(Guid cityId)
        {
            return await _context.Properties
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .Where(x => x.CityId == cityId)
                .ToListAsync();
        }

    }
}
