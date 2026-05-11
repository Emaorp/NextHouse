using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.Utilites.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Commands.Loging
{
    public class LoginUseCase : IRequestHandler<LoginCommand, AccountSignInResult>
    {
        private readonly IAccountRepository _accountRepository;

        public LoginUseCase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccountSignInResult> Handle(LoginCommand request)
        {
            return _accountRepository.SignInAsync(request.UserName, request.Password, request.RememberMe);
        }
    }
}
