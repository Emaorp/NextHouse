using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Application.UseCases.PropertyRequest.Commands.Update
{
    public class UpdatePropertyRequestCommand : IRequest<bool>
    {
        public UpdatePropertyRequestDto Dto { get; set; }

        public UpdatePropertyRequestCommand(UpdatePropertyRequestDto dto)
        {
            Dto = dto;
        }
    }
}