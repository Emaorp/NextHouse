using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.UseCases.Property.Commands.CreateProperty;
using NextHouse.Application.UseCases.PropertyRequest.Commands.Create;
using NextHouse.Application.UseCases.PropertyRequest.Commands.CreateRequest;
using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Property
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto dto)
        {
            var command = new CreateRequestCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}
