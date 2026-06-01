using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Application.UseCases.PropertyRequest.Querys.Get
{
    public class GetPropertyRequestUseCase : IRequestHandler<GetPropertyRequestQuery, PropertyRequestResponseDto>
    {
        private readonly IPropertyRequestRepository _repository;

        public GetPropertyRequestUseCase(IPropertyRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropertyRequestResponseDto> Handle(GetPropertyRequestQuery request)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                throw new Exception("Solicitud no encontrada");

            return new PropertyRequestResponseDto
            {
                Id = entity.Id,
                PropertyId = entity.PropertyId,
                PropertyTitle = entity.Property.Title,
                ApplicationsName = entity.ApplicationsName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Message = entity.Message,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}