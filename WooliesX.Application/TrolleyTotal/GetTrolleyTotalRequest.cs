using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Entities.Trolley;

namespace WooliesX.Application.TrolleyTotal
{
    public class GetTrolleyTotalRequest : IRequest<string>
    {
        public TrolleyDto Trolley { get; set; }
    }

    public class GetTrolleyTotalRequestHandler : IRequestHandler<GetTrolleyTotalRequest, string>
    {
        private readonly ITrolleyRepository _trolleyRepository;
        private readonly IMapper _mapper;

        public GetTrolleyTotalRequestHandler(ITrolleyRepository trolleyRepository, IMapper mapper)
        {
            _trolleyRepository = trolleyRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(GetTrolleyTotalRequest request, CancellationToken cancellationToken)
        {
            var trolleyEntity = _mapper.Map<TrolleyEntity>(request.Trolley);
            return await _trolleyRepository.GetTrolleyTotal(trolleyEntity).ConfigureAwait(false);
        }
    }
}
