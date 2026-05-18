using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextHouse.Application.Contracts.Security;
using NextHouse.Application.UseCases.City.Queries.GetCities;
using NextHouse.Application.UseCases.Department.Queries.GetDerpartments;
using NextHouse.Application.UseCases.Property.Commands.CreateProperty;
using NextHouse.Application.UseCases.Property.Commands.DeleteProperty;
using NextHouse.Application.UseCases.Property.Commands.UpdateProperty;
using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;
using NextHouse.Application.UseCases.Property.Queries.GetPropertyByID;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.DTOs.Properties;
using NextHouse.Web.Security;
using System.Security.Claims;

namespace NextHouse.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PropertyController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        // =========================================
        // GET: Property/Create
        // =========================================
        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.CREATE_PROPERTIES)]
        public async Task<IActionResult> Create()
        {
            await LoadDepartmentsAsync();
            return View();
        }

        // =========================================
        // POST: Property/Create
        // =========================================
        [HttpPost]

        [RequirePermission(PermissionCodesCatalog.CREATE_PROPERTIES)]
        public async Task<IActionResult> Create(
            CreatePropertyDto dto,
            List<IFormFile> Images)
        {
            if (!ModelState.IsValid)
            {
                await LoadDepartmentsAsync();
                return View(dto);
            }

            // Guardar imágenes en wwwroot/uploads/properties (máximo 10)
            var imageUrls = new List<string>();
            var allowedImages = Images
                .Where(f => f != null && f.Length > 0)
                .Take(10)
                .ToList();

            foreach (var file in allowedImages)
            {
                var uploadsFolder = Path.Combine(
                    _webHostEnvironment.WebRootPath, "uploads", "properties");

                Directory.CreateDirectory(uploadsFolder);

                var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                imageUrls.Add($"/uploads/properties/{uniqueName}");
            }

            dto.ImageUrls = imageUrls;
            dto.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            CreatePropertyCommand command = new(dto);
            Guid propertyId = await _mediator.Send(command);

            return RedirectToAction(
                actionName: "Details",
                controllerName: "Property",
                routeValues: new { id = propertyId });
        }

        // =========================================
        // GET: Property/Details/{id}
        // =========================================
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            GetPropertyByIdQuery query = new() { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return View(result);
        }

        // =========================================
        // GET: Property/GetById/{id}  (mantener por compatibilidad)
        // =========================================
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            return RedirectToAction("Details", new { id });
        }

        // =========================================
        // POST: Property/Filters
        // =========================================
        [HttpPost]
        public async Task<IActionResult> Filters(
            [FromBody] GetPropertiesByFiltersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // =========================================
        // PUT: Property/Update/{id}
        // =========================================
        [HttpPut]
        [RequirePermission(PermissionCodesCatalog.EDIT_PROPERTIES)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdatePropertyCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID no coincide.");

            bool result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return Ok();
        }

        // =========================================
        // DELETE: Property/Delete/{id}
        // =========================================
        [HttpDelete]
        [RequirePermission(PermissionCodesCatalog.DELETE_PROPERTIES)]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeletePropertyCommand command = new(id);
            bool result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(Guid departmentId)
        {
            GetCityQuery query = new() { DepartmentId = departmentId };
            var cities = await _mediator.Send(query);
            return Json(cities);
        }

        // =========================================
        // PRIVATE METHODS
        // =========================================
        private async Task LoadDepartmentsAsync()
        {
            var departments = await _mediator.Send(new GetDepartmentsQuery());

            ViewBag.Departments = departments
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                })
                .ToList();
        }
    }
}
