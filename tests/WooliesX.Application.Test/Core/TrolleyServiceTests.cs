using Moq;
using System;
using System.Collections.Generic;
using WooliesX.Application.Common.Interfaces;
using WooliesX.Application.Core;
using WooliesX.Domain.Entities.Trolley;
using Xunit;

namespace WooliesX.Application.Test
{
    public class TrolleyServiceTests : IDisposable
    {
        private readonly Mock<ITrolleyRepository> _mockTrolleyRepository;
        private readonly ITrolleyService _sut;

        public TrolleyServiceTests()
        {
            _mockTrolleyRepository = new Mock<ITrolleyRepository>(MockBehavior.Strict);
            _sut = new TrolleyService(_mockTrolleyRepository.Object);
        }

        public void Dispose()
        {
            _mockTrolleyRepository.VerifyAll();
        }

        [Fact]
        public void GetTrolleyTotal_ShouldGetTrolleyTotalForTrolleyEntity()
        {
            var entity = new TrolleyEntity
            {
                Products = new List<TrolleyProductEntity>
                {
                    new TrolleyProductEntity { Name = "Foo Product", Price = 1 }
                },
                Quantities = new List<TrolleyProductQuantityEntity>
                {
                    new TrolleyProductQuantityEntity { Name = "Foo Product Quantity", Quantity = 1 }
                },
                Specials = new List<TrolleySpecialEntity>
                {
                    new TrolleySpecialEntity {
                        Quantities = new List<TrolleyProductQuantityEntity>
                        {
                            new TrolleyProductQuantityEntity { Name = "Foo Special Product Quantity", Quantity = 1 }
                        },
                        Total = 1
                    }
                }
            };
            var expectedTotal = "123";

            _mockTrolleyRepository.Setup(s => s.GetTrolleyTotal(entity)).ReturnsAsync(expectedTotal);

            var result = _sut.GetTrolleyTotal(entity).Result;

            Assert.Equal(expectedTotal, result);
        }
    }
}
