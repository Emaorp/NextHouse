using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(DeletePropertyCommand request)
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