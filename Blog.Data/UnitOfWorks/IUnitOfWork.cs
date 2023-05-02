using Blog.Core.Entities;
using Blog.Data.Repositories.Abstractions;
using Blog.Entitiy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IEntitiyBase, new();

        Task<int> SaveAsync();
        int Save();
    }
}
