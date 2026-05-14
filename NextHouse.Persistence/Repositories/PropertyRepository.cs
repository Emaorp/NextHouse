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
            await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Property property)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Property>> GetAllByCityAsync(Guid cityId)
        {
            return await _context.Properties
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .Where(x => x.CityId == cityId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetByFiltersAsync(GetPropertiesByFiltersQuery filters)
        {
            var query = _context.Properties
                .Include(x => x.City)
                .ThenInclude(x => x.Department)
                .Include(x => x.Images)
                .AsQueryable();

            // 1. Filtrar solo disponibles
            query = query.Where(x => x.Status == PropertyStatus.Available);

            // 2. FILTRO POR CIUDAD (Corregido: Validamos que no sea Guid vacío)
            if (filters.CityId.HasValue && filters.CityId.Value != Guid.Empty)
            {
                query = query.Where(x => x.CityId == filters.CityId.Value);
            }

            // 3. FILTRO POR TIPO (Corregido para manejar "0" y "1")
            if (!string.IsNullOrWhiteSpace(filters.PropertyType))
            {
                if (int.TryParse(filters.PropertyType, out int typeInt))
                {
                    // Si el frontend manda "0" o "1", lo comparamos numéricamente con el Enum
                    query = query.Where(x => (int)x.Type == typeInt);
                }
                else
                {
                    // Por si acaso mandan "Rent" o "Sale" como texto
                    query = query.Where(x => x.Type.ToString().ToLower() == filters.PropertyType.ToLower());
                }
            }

            // 4. RANGO DE PRECIO
            if (filters.MinPrice.HasValue && filters.MinPrice.Value > 0)
            {
                query = query.Where(x => x.Price >= (decimal)filters.MinPrice.Value);
            }

            if (filters.MaxPrice.HasValue && filters.MaxPrice.Value > 0)
            {
                query = query.Where(x => x.Price <= (decimal)filters.MaxPrice.Value);
            }

            // 5. HABITACIONES Y BAÑOS
            if (filters.Bedrooms.HasValue && filters.Bedrooms.Value > 0)
            {
                query = query.Where(x => x.Bedrooms == filters.Bedrooms.Value);
            }

            if (filters.Bathrooms.HasValue && filters.Bathrooms.Value > 0)
            {
                query = query.Where(x => x.Bathrooms == filters.Bathrooms.Value);
            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}