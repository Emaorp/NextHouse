using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;

namespace NextHouse.Application.UseCases.PropertyRequest.Commands.Update
{
    public class UpdatePropertyRequestUseCase : IRequestHandler<UpdatePropertyRequestCommand, bool>
    {
        private readonly IPropertyRequestRepository _repository;

        public UpdatePropertyRequestUseCase(IPropertyRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdatePropertyRequestCommand request)
        {
            var entity = await _repository.GetByIdAsync(request.Dto.Id);

            if (entity == null)
                return false;

            entity.Status = request.Dto.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}