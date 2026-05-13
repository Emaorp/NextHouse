using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Queries.GetAccountUserInfo
{
    public class GetAccountUserInfoUseCase : IRequestHandler<GetAccountUserInfoQuery, UserAccountInfoDTO>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountUserInfoUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<UserAccountInfoDTO> Handle(GetAccountUserInfoQuery request)
        {
            return _accountRepository.GetUserInfoAsync(request.UserId);
        }
    }
}
