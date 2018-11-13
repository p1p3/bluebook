using System;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Core.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
