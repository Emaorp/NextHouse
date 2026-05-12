using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Users.Commands.CreateUser
{
    public sealed class CreateUserUseCase : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<string> Handle(CreateUserCommand command)
        {
            User user = User.Reconstitute(
                                       id: Guid.CreateVersion7().ToString(),
                                       firstName: command.FirstName,
                                       lastName: command.LastName,
                                       userName: command.Email,
                                       email: command.Email,
                                       emailConfirmed: true,
                                       phoneNumber: command.PhoneNumber,
                                       roleId: command.RoleId);

            await _usersRepository.CreateAsync(user, command.Password);

            return user.Id;
        }
    }
}
