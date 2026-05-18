using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.UseCases.PropertyRequest.Commands.Create;
using NextHouse.Application.UseCases.PropertyRequest.Commands.CreateRequest;
using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Web.Controllers
{
    public class PropertyRequestController : Controller
    {
        private readonly IMediator _mediator;

        public PropertyRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // =========================================
        // POST: api/PropertyRequest (API endpoint)
        // =========================================
        [HttpPost]
        [Route("api/PropertyRequest")]
        public async Task<IActionResult> CreateApi([FromBody] CreateRequestDto dto)
        {
            var command = new CreateRequestCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }

        // =========================================
        // POST: PropertyRequest/Create (MVC form)
        // =========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["RequestError"] = "Por favor completa todos los campos requeridos.";
                return RedirectToAction("Details", "Property", new { id = dto.PropertyId });
            }

            var command = new CreateRequestCommand(dto);
            await _mediator.Send(command);

            TempData["RequestSuccess"] = "¡Tu solicitud fue enviada exitosamente! Pronto nos comunicaremos contigo.";
            return RedirectToAction("Details", "Property", new { id = dto.PropertyId });
        }
    }
}
