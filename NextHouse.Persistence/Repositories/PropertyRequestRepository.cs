using Microsoft.EntityFrameworkCore;
using NextHouse.Application.Contracts.Persistence;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Domain.Entities.Properties;
using NextHouse.Domain.Entities.Request;


namespace NextHouse.Persistence.Repositories;

public class PropertyRequestRepository
    : IPropertyRequestRepository
{
    private readonly DataContext _context;

    public PropertyRequestRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<PropertyRequest?> GetByIdAsync(Guid id)
    {
        return await _context.PropertyRequests
            .Include(x => x.Property)
            .ThenInclude(x => x.City)
            .ThenInclude(x => x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<PropertyRequest>> GetAllAsync()
    {
        return await _context.PropertyRequests
            .Include(x => x.Property)
            .ThenInclude(x => x.City)
            .ThenInclude(x => x.Department)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<PropertyRequest>>
        GetByPropertyIdAsync(Guid propertyId)
    {
        return await _context.PropertyRequests
            .Include(x => x.Property)
            .ThenInclude(x => x.City)
            .Where(x => x.PropertyId == propertyId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(PropertyRequest request)
    {
        await _context.PropertyRequests.AddAsync(request);
        await _context.SaveChangesAsync(); // Guarda en la BD
    }

    public void Update(PropertyRequest request)
    {
        _context.PropertyRequests.Update(request);
    }

    public void Delete(PropertyRequest request)
    {
        _context.PropertyRequests.Remove(request);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.PropertyRequests
            .AnyAsync(x => x.Id == id);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}