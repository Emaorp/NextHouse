using MediatR;
using NextHouse.Application.Contracts.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.DeleteProperty
{
    public class DeletePropertyCommandHandler : global::MediatR.IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
          
            var property = await _propertyRepository.GetByIdAsync(request.Id);

            
            if (property == null)
            {
                return false;
            }

            await _propertyRepository.DeleteAsync(property);

            return true;
        }
    }
}