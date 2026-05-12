
using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.UseCases.Property.Commands.CreateProperty;
using NextHouse.Application.UseCases.Property.Commands.UpdateProperty;
using NextHouse.Application.UseCases.Property.Commands.DeleteProperty;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Threading.Tasks;

namespace NextHouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/Property
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            var command = new CreatePropertyCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/Property/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePropertyCommand command)
        {
            if (id != command.Id) return BadRequest("El ID no coincide");

            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        // DELETE: api/Property/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePropertyCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }
    }
}