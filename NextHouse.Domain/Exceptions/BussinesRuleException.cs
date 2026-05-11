using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Domain.Exceptions
{
    public class BussinesRuleException : Exception
    {
        public BussinesRuleException(string message) : base(message)
        {

        }
    }
}
