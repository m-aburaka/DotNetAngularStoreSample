using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAngularStoreSample.Application.Repositories
{
    public interface IRepository<T>
    {
        Task<bool> Exists(int id);
        Task<IList<T>> Get();
        Task<T> Get(int id);
        Task Insert(T entity);
        Task Delete(T entity);
    }
}
