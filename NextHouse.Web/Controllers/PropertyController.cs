
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

namespace NextHouse.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
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
        [ValidateAntiForgeryToken]
        [RequirePermission(PermissionCodesCatalog.CREATE_PROPERTIES)]
        public async Task<IActionResult> Create(CreatePropertyDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCitiesAsync();

                return View(dto);
            }

            CreatePropertyCommand command = new(dto);

            Guid propertyId = await _mediator.Send(command);

            return RedirectToAction(
                actionName: "GetById",
                controllerName: "Property",
                routeValues: new { id = propertyId });
        }

        // =========================================
        // GET: Property/GetById/{id}
        // =========================================
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            GetPropertyByIdQuery query = new()
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
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
            {
                return BadRequest("El ID no coincide.");
            }

            bool result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

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
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetCities(Guid departmentId)
        {
            GetCityQuery query = new()
            {
                DepartmentId = departmentId
            };

            var cities = await _mediator.Send(query);

            return Json(cities);
        }

        // =========================================
        // PRIVATE METHODS
        // =========================================
        private async Task LoadCitiesAsync()
        {
            var cities = await _mediator.Send(new GetCityQuery());

            ViewBag.Cities = cities
                .Select(city => new SelectListItem
                {
                    Value = city.Id.ToString(),
                    Text = city.Name
                })
                .ToList();
        }
        // =========================================
        // PRIVATE METHODS
        // =========================================
        private async Task LoadDepartmentsAsync()
        {
            var departments = await _mediator.Send(new GetDepartmentsQuery());

            ViewBag.Departments = departments
                .Select(department => new SelectListItem
                {
                    Value = department.Id.ToString(),
                    Text = department.Name
                })
                .ToList();
        }
    }
}