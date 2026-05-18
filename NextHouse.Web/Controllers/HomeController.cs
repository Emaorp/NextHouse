using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextHouse.Application.Contracts.Security;
using NextHouse.Application.UseCases.City.Queries.GetCities;
using NextHouse.Application.UseCases.Department.Queries.GetDerpartments;
using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.DTOs.Properties;
using NextHouse.Web.Models;
using NextHouse.Web.Security;
using System.Diagnostics;

namespace NextHouse.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var departments = await _mediator.Send(new GetDepartmentsQuery());

            var vm = new PropertyFilterViewModel
            {
                Departments = departments.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };



            return View(vm);
        }

        [HttpGet]
        public async Task<JsonResult> GetCities(Guid departmentId)
        {
            var cities = await _mediator.Send(
                new GetCityQuery
                {
                    DepartmentId = departmentId
                });

            return Json(cities);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PropertyFilterViewModel model)
        {
            string propertyType = null ;
            if (model.PropertyType != null)
            {
                propertyType = model.PropertyType.ToString();
            }
            var query = new GetPropertiesByFiltersQuery
            {
                CityId = model.CityId,
                PropertyType = propertyType,
                MinPrice = Convert.ToDouble(model.MinPrice),
                MaxPrice = Convert.ToDouble(model.MaxPrice),
                Bedrooms = model.Bedrooms,
                Bathrooms = model.Bathrooms
            };

            var properties = await _mediator.Send(query);

            var departments = await _mediator.Send(new GetDepartmentsQuery());

            model.Departments = departments.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            model.Properties = properties;

            return View(model);
        }
    }
}
