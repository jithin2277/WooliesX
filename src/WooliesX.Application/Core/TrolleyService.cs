using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities.Trolley;

namespace WooliesX.Application.Core
{
    public interface ITrolleyService
    {
        Task<string> GetTrolleyTotal(TrolleyEntity trolleyEntity);
    }

    public class TrolleyService : ITrolleyService
    {
        private readonly ITrolleyRepository _trolleyRepository;

        public TrolleyService(ITrolleyRepository trolleyRepository)
        {
            _trolleyRepository = trolleyRepository;
        }

        public async Task<string> GetTrolleyTotal(TrolleyEntity trolleyEntity)
        {
            return await _trolleyRepository.GetTrolleyTotal(trolleyEntity).ConfigureAwait(false);
        }
    }
}
