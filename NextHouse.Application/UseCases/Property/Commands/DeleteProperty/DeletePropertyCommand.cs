using NextHouse.Application.Utilites.Mediator;
using System;

namespace NextHouse.Application.UseCases.Property.Commands.DeleteProperty
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