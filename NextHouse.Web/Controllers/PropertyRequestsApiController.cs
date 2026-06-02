using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.UseCases.PropertyRequest.Commands.CreateRequest;
using NextHouse.Application.UseCases.PropertyRequest.Commands.Update;

using NextHouse.Application.UseCases.PropertyRequest.Queries.GetRequestByAgentId;
using NextHouse.Application.UseCases.PropertyRequest.Querys.Get;

namespace NextHouse.Web.Controllers
{
    [ApiController]
    [Route("api/property-requests")] 
    public class PropertyRequestsApiController : ControllerBase
    {
       
        private readonly GetPropertyRequestUseCase _getUseCase;
        private readonly GetPropertyRequestByAgentIdUseCase _getByAgentUseCase;
        private readonly CreateRequestCommandUseCase _createUseCase;
        private readonly UpdatePropertyRequestUseCase _updateUseCase;

        public PropertyRequestsApiController(
            GetPropertyRequestUseCase getUseCase,
            GetPropertyRequestByAgentIdUseCase getByAgentUseCase,
            CreateRequestCommandUseCase createUseCase,
            UpdatePropertyRequestUseCase updateUseCase)
        {
            _getUseCase = getUseCase;
            _getByAgentUseCase = getByAgentUseCase;
            _createUseCase = createUseCase;
            _updateUseCase = updateUseCase;
        }

        
[HttpGet("{id}")]
public async Task<IActionResult> GetById(Guid id) 
{
    
    var query = new GetPropertyRequestQuery { Id = id }; 
    
    var result = await _getUseCase.Handle(query);
    
    if (result == null)
        return NotFound();

    return Ok(result);
}

     
        [HttpGet("agent/{agentId}")]
        public async Task<IActionResult> GetByAgentId(string agentId)
        {
            var query = new GetPropertyRequestByAgentIdQuery { AgentId = agentId }; 
            var requests = await _getByAgentUseCase.Handle(query);
            return Ok(requests);
        }

   
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            var newRequestId = await _createUseCase.Handle(command);
            return Created("", new { id = newRequestId, message = "Solicitud de propiedad creada con éxito." });
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePropertyRequestDto dto)
        {

            if (id != dto.Id.ToString())
            {
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del objeto enviado." });
            }

            var command = new UpdatePropertyRequestCommand(dto);

          
            await _updateUseCase.Handle(command);

            return NoContent(); 
        }
    }
}