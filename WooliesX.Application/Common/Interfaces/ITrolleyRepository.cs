using System.Threading.Tasks;
using WooliesX.Domain.Entities.Trolley;

namespace WooliesX.Application.Common.Interfaces
{
    public interface ITrolleyRepository
    {
        Task<string> GetTrolleyTotal(TrolleyEntity trolleyEntity);
    }
}
