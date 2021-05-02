using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Application.Core;
using WooliesX.Domain.Entities.Trolley;

namespace WooliesX.Application.TrolleyTotal
{
    public class GetTrolleyTotalRequest : IRequest<string>
    {
        public TrolleyDto Trolley { get; set; }
    }

    public class GetTrolleyTotalRequestHandler : IRequestHandler<GetTrolleyTotalRequest, string>
    {
        private readonly ITrolleyService _trolleyService;
        private readonly IMapper _mapper;

        public GetTrolleyTotalRequestHandler(ITrolleyService trolleyService, IMapper mapper)
        {
            _trolleyService = trolleyService;
            _mapper = mapper;
        }

        public async Task<string> Handle(GetTrolleyTotalRequest request, CancellationToken cancellationToken)
        {
            var trolleyEntity = _mapper.Map<TrolleyEntity>(request.Trolley);
            return await _trolleyService.GetTrolleyTotal(trolleyEntity).ConfigureAwait(false);
        }
    }
}
