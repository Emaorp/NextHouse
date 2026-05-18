using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextHouse.Persistence.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly DataContext _context;

        public PropertyImageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PropertyImage?> GetByIdAsync(Guid id)
        {
            return await _context.PropertyImages
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(Guid propertyId)
        {
            return await _context.PropertyImages
                .Where(x => x.PropertyId == propertyId)
                .OrderByDescending(x => x.IsPrimary)
                .ToListAsync();
        }

        public async Task<PropertyImage?> GetPrimaryImageAsync(Guid propertyId)
        {
            return await _context.PropertyImages
                .Where(x => x.PropertyId == propertyId && x.IsPrimary)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(PropertyImage image)
        {
            await _context.PropertyImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<PropertyImage> images)
        {
            await _context.PropertyImages.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PropertyImage image)
        {
            _context.PropertyImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PropertyImage image)
        {
            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<PropertyImage> images)
        {
            _context.PropertyImages.RemoveRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.PropertyImages.AnyAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
