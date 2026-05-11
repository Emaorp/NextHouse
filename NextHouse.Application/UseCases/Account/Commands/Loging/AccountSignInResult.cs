using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.UseCases.Account.Commands.Loging
{
    public class AccountSignInResult
    {
        public required bool Succeeded { get; set; }
        public required bool IsLockedOut { get; set; }

        public bool InvalidCredentials => !Succeeded && !IsLockedOut;
    }
}
