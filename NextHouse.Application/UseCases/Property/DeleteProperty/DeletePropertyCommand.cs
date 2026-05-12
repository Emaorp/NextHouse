using MediatR;
using System;

namespace NextHouse.Application.UseCases.Property.DeleteProperty
{
    public class DeletePropertyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeletePropertyCommand(Guid id)
        {
            Id = id;
        }
    }
}