using System;
using System.Collections.Generic;
using System.Text;

namespace NextHouse.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
