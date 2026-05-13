using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.UserHasPermission
{
    public class UserHasPermissionUseCase : IRequestHandler<UserHasPermissionQuery, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public UserHasPermissionUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<bool> Handle(UserHasPermissionQuery request)
        {
            return _accountRepository.UserHasPermissionAsync(request.UserId, request.PermissionCode);
        }
    }
}
