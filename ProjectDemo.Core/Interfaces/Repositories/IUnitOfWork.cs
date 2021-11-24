using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }

        void Commit();
        void Rollback();
    }
}
