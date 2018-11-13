using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Core.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
