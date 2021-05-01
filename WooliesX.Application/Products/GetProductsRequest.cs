using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Domain.Enums;

namespace WooliesX.Application.Products
{
    public class GetProductsRequest : IRequest<IEnumerable<ProductDto>>
    {
        public SortOption? SortOption { get; set; }
    }

    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, IEnumerable<ProductDto>>
    {
        private readonly IProductRespository _productRespository;
        private readonly IShopperRespository _shopperRespository;
        private readonly IMapper _mapper;

        public GetProductsRequestHandler(IProductRespository productRespository, IShopperRespository shopperRespository, IMapper mapper)
        {
            _productRespository = productRespository;
            _shopperRespository = shopperRespository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IEnumerable<ProductDto>>(await _productRespository.GetProducts());
            if (request.SortOption.HasValue)
            {
                var sortOption = request.SortOption.Value;
                return sortOption switch
                {
                    SortOption.Low => products.OrderBy(o => o.Price),
                    SortOption.High => products.OrderByDescending(o => o.Price),
                    SortOption.Ascending => products.OrderBy(o => o.Name),
                    SortOption.Descending => products.OrderByDescending(o => o.Name),
                    SortOption.Recommended => await SortProductsByPopularity(products).ConfigureAwait(false),
                    _ => products,
                };
            }

            return products;
        }

        private async Task<IEnumerable<ProductDto>> SortProductsByPopularity(IEnumerable<ProductDto> products)
        {
            var shopperHistory = await _shopperRespository.GetShopperHistory().ConfigureAwait(false);
            var shopperHistoryProducts = _mapper.Map<IEnumerable<ProductDto>>(shopperHistory.SelectMany(s => s.Products));

            return shopperHistoryProducts
                .Concat(products)
                .GroupBy(g => g.Name)
                .Select(s => new ProductDto
                {
                    Name = s.Key,
                    Price = s.Where(w => w.Name == s.Key).First().Price,
                    Quantity = s.Sum(u => u.Quantity)
                })
                .OrderByDescending(o => o.Quantity);
        }
    }
}
