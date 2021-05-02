using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using WooliesX.Application.Common.Mappings;
using WooliesX.Application.Core;
using WooliesX.Application.TrolleyTotal;
using WooliesX.Domain.Entities.Trolley;
using Xunit;

namespace WooliesX.Application.Test
{
    public class GetTrolleyTotalRequestHandlerTests : IDisposable
    {
        private readonly Mock<ITrolleyService> _mockTrolleyService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetTrolleyTotalRequestHandler _sut;

        public GetTrolleyTotalRequestHandlerTests()
        {
            _mockTrolleyService = new Mock<ITrolleyService>(MockBehavior.Strict);
            _mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            _sut = new GetTrolleyTotalRequestHandler(_mockTrolleyService.Object, _mockMapper.Object);
        }

        public void Dispose()
        {
            _mockTrolleyService.VerifyAll();
        }

        [Fact]
        public void Handle_ShouldGetTrolleyTotal()
        {
            var expected = "123";

            var trolley = new TrolleyDto
            {
                Products = new List<TrolleyProduct>
                {
                    new TrolleyProduct { Name = "TP1", Price = 1 },
                    new TrolleyProduct { Name = "TP2", Price = 2 },
                    new TrolleyProduct { Name = "TP3", Price = 3 }
                },
                Quantities = new List<TrolleyProductQuantity>
                 {
                     new TrolleyProductQuantity { Name = "TPQ1", Quantity = 1 },
                     new TrolleyProductQuantity { Name = "TPQ2", Quantity = 2 },
                     new TrolleyProductQuantity { Name = "TPQ3", Quantity = 3 }
                 },
                Specials = new List<TrolleySpecial>
                {
                    new TrolleySpecial {
                        Quantities =  new List<TrolleyProductQuantity>
                        {
                            new TrolleyProductQuantity { Name = "TPQS1", Quantity = 1 },
                            new TrolleyProductQuantity { Name = "TPQS2", Quantity = 2 },
                            new TrolleyProductQuantity { Name = "TPQS3", Quantity = 3 }
                        },
                        Total = 3
                    }
                }
            };

            var request = new GetTrolleyTotalRequest
            {
                Trolley = trolley
            };

            var mapper = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            }).CreateMapper();
            var entity = mapper.Map<TrolleyEntity>(request.Trolley);
            _mockMapper.Setup(s => s.Map<TrolleyEntity>(request.Trolley)).Returns(entity);

            _mockTrolleyService.Setup(s => s.GetTrolleyTotal(entity)).ReturnsAsync(expected);

            var result = _sut.Handle(request, It.IsAny<CancellationToken>()).Result;

            Assert.Equal(expected, result);
        }
    }
}
