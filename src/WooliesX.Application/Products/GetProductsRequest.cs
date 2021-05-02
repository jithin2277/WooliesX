using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Application.Core;
using WooliesX.Domain.Enums;

namespace WooliesX.Application.Products
{
    public class GetProductsRequest : IRequest<IEnumerable<ProductDto>>
    {
        public SortOption? SortOption { get; set; }
    }

    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, IEnumerable<ProductDto>>
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public GetProductsRequestHandler(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _productsService.GetProducts(request.SortOption).ConfigureAwait(false);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
