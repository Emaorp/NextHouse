
using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.Contracts.Security;
using NextHouse.Application.UseCases.Property.Commands.CreateProperty;
using NextHouse.Application.UseCases.Property.Commands.DeleteProperty;
using NextHouse.Application.UseCases.Property.Commands.UpdateProperty;
using NextHouse.Application.UseCases.Property.Queries.GetPropertiesListByFilters;
using NextHouse.Application.UseCases.Property.Queries.GetPropertyByID;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.Security;
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
        [RequirePermission(PermissionCodesCatalog.CREATE_PROPERTIES)]

        public async Task<IActionResult> Create([FromBody] CreatePropertyDto dto)
        {
            var command = new CreatePropertyCommand(dto);
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // PUT: api/Property/{id}
        [HttpPut("{id}")]
        [RequirePermission(PermissionCodesCatalog.EDIT_PROPERTIES)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePropertyCommand command)
        {
            if (id != command.Id) return BadRequest("El ID no coincide");

            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        // DELETE: api/Property/{id}
        [HttpDelete("{id}")]
        [RequirePermission(PermissionCodesCatalog.DELETE_PROPERTIES)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePropertyCommand(id);
            var result = await _mediator.Send(command);
            return result ? Ok() : NotFound();
        }

        [HttpPost("filters")]
        public async Task<IActionResult> GetByFilters(
      [FromBody] GetPropertiesByFiltersQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetPropertyByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }


    }
}