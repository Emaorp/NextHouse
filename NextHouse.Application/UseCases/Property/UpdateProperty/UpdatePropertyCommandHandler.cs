using MediatR;
using NextHouse.Application.Contracts.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextHouse.Application.UseCases.Property.UpdateProperty
{
    public class UpdatePropertyCommandHandler : global::MediatR.IRequestHandler<UpdatePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;

        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            
            var property = await _propertyRepository.GetByIdAsync(request.Id);

            if (property == null)
            {
                return false; 
            }

            
            property.Title = request.Title;
            property.Description = request.Description;
            property.Price = request.Price;
            property.Bedrooms = request.Bedrooms;
            property.Bathrooms = request.Bathrooms;
            property.Area = request.Area;
            property.Address = request.Address;
            property.Neighborhood = request.Neighborhood;
            property.UpdatedAt = DateTime.Now;

            
            await _propertyRepository.UpdateAsync(property);

            return true;
        }
    }
}