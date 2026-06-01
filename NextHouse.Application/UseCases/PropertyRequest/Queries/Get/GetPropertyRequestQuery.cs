using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Application.UseCases.PropertyRequest.Querys.Get
{
    public class GetPropertyRequestQuery : IRequest<PropertyRequestResponseDto>
    {
        public Guid Id { get; set; }
    }
}