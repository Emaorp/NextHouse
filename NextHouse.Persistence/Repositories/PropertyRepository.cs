using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;
using NextHouse.Domain.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _context.SaveChangesAsync(); // Guarda en la BD
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
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync(); // Guarda en la BD
        }

        public async Task DeleteAsync(Property property)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync(); // Guarda en la BD
        }

        public async Task<IEnumerable<Property>> GetAllByCityAsync(Guid cityId)
        {
            return await _context.Properties
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .Where(x => x.CityId == cityId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetByFiltersAsync(
    GetPropertiesByFiltersQuery filters)
        {
            var query = _context.Properties
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .Include(x => x.Images)
                .AsQueryable();

            query = query.Where(x =>
                x.Status == PropertyStatus.Available);

            if (filters.CityId.HasValue)
            {
                query = query.Where(x =>
                    x.CityId == filters.CityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.PropertyType))
            {
                query = query.Where(x =>
             x.Type.ToString().ToLower() ==
             filters.PropertyType.ToLower());
            }

            // PRICE RANGE
            if (filters.MinPrice.HasValue &&
                filters.MaxPrice.HasValue)
            {
                query = query.Where(x =>
                    x.Price >= (decimal)filters.MinPrice.Value &&
                    x.Price <= (decimal)filters.MaxPrice.Value);
            }

            if (filters.Bedrooms.HasValue)
            {
                query = query.Where(x =>
                    x.Bedrooms == filters.Bedrooms.Value);
            }

            if (filters.Bathrooms.HasValue)
            {
                query = query.Where(x =>
                    x.Bathrooms == filters.Bathrooms.Value);
            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }



    }
}