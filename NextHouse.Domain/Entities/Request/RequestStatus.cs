using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Domain.Entities.Request
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
}
