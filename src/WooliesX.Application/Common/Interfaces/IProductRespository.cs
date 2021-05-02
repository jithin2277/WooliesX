using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Domain.Entities;

namespace WooliesX.Application.Common.Interfaces
{
    public interface IProductRespository
    {
        Task<IEnumerable<ProductEntity>> GetProducts();
    }
}
