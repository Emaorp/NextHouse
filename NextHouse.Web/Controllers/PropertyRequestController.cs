using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.Contracts.Security;
using NextHouse.Application.UseCases.PropertyRequest.Commands.Create;
using NextHouse.Application.UseCases.PropertyRequest.Commands.CreateRequest;
using NextHouse.Application.UseCases.PropertyRequest.Queries.GetRequestByAgentId;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Entities.Request;
using NextHouse.Web.Security;
using NextHouse.Application.UseCases.PropertyRequest.Querys.Get;

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
        /*[HttpPost]
        [Route("api/PropertyRequest")]

        public async Task<IActionResult> CreateApi([FromBody] CreateRequestDto dto)
        {
            var command = new CreateRequestCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }
        */
    
        [HttpPost]
       // [RequirePermission(PermissionCodesCatalog.CREATE_REQUESTS)]
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

        
        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.SHOW_REQUESTS)]
        public async Task<IActionResult> MyRequests()
        {
            string? agentId =
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
                ?.Value;

            if (string.IsNullOrWhiteSpace(agentId))
            {
                return RedirectToAction("Login", "Account");
            }

            GetPropertyRequestByAgentIdQuery query =
                new GetPropertyRequestByAgentIdQuery
                {
                    AgentId = agentId
                };

            IReadOnlyList<GetPropertyRequestByAgentIdResponseDTO> result =
                await _mediator.Send(query);

            return View(result);
        }

        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.SHOW_REQUESTS)]
        public async Task<IActionResult> Details(Guid id)
        {
            var query = new NextHouse.Application.UseCases.PropertyRequest.Querys.Get.GetPropertyRequestQuery
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermission(PermissionCodesCatalog.EDIT_REQUESTS)]
        public async Task<IActionResult> UpdateStatus(Guid id, NextHouse.Domain.Entities.Request.RequestStatus status)
        {
            var dto = new NextHouse.Application.UseCases.PropertyRequest.Commands.Update.UpdatePropertyRequestDto
            {
                Id = id,
                Status = status
            };

            var command = new NextHouse.Application.UseCases.PropertyRequest.Commands.Update.UpdatePropertyRequestCommand(dto);
            await _mediator.Send(command);

            TempData["Success"] = "Estado actualizado correctamente.";
            return RedirectToAction("Details", new { id });
        }

    }
}
