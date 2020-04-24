using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowDominioShared.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();

        bool Commit();
    }
}
