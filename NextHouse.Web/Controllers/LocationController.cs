using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextHouse.Persistence;
using System.Threading.Tasks;

namespace NextHouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly DataContext _context;

        public LocationController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Location/departments
        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _context.Departments
                .Select(d => new
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .OrderBy(d => d.Name)
                .ToListAsync();

            return Ok(departments);
        }

        // GET: api/Location/departments/{departmentId}/cities
        [HttpGet("departments/{departmentId:guid}/cities")]
        public async Task<IActionResult> GetCitiesByDepartment(Guid departmentId)
        {
            var cities = await _context.Cities
                .Where(c => c.DepartmentId == departmentId)
                .Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(cities);
        }
    }
}