using NextHouse.Domain.Entities.Request;

namespace NextHouse.Application.UseCases.PropertyRequest.Commands.Update
{
    public class UpdatePropertyRequestDto
    {
        public Guid Id { get; set; }
        public RequestStatus Status { get; set; }
    }
}