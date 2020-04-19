using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WooliesX.Data.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> 
    }
}
