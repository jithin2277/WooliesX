using Microsoft.VisualStudio.TestTools.UnitTesting;
using WooliesX.Data.Entities;
using WooliesX.Http;
using System.Linq;
using WooliesX.Data.Entities.Trolley;
using System.Collections.Generic;

namespace WooliesX.Data.IntegrationTests
{
    [TestClass]
    public class TrolleyTotalProcessorTests
    {
        private ITrolleyTotalProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new TrolleyTotalProcessor(
                new HttpClientHelper()
            );
        }

        [TestMethod]
        public void GetTrolleyTotal_ReturnsTrolleyTotalFromApi()
        {
            var expected = "8.97";

            var trolleyProductEntities = new List<TrolleyProductEntity> {
                new TrolleyProductEntity {
                    Name = "A",
                    Price = 1.99
                },
                new TrolleyProductEntity {
                    Name = "B",
                    Price = 2.99
                },
                new TrolleyProductEntity {
                    Name = "C",
                    Price = 3.99
                }
            };
            var trolleySpecialProductQuantityEntitiesSpecials = new List<TrolleyProductQuantityEntity> {
                new TrolleyProductQuantityEntity {
                    Name = "A",
                    Quantity = 1
                },
                new TrolleyProductQuantityEntity {
                    Name = "B",
                    Quantity = 1
                },
                new TrolleyProductQuantityEntity {
                    Name = "C",
                    Quantity = 1
                }
            };
            var trolleySpecialProductQuantityEntities = new List<TrolleyProductQuantityEntity> {
                new TrolleyProductQuantityEntity {
                    Name = "A",
                    Quantity = 1
                },
                new TrolleyProductQuantityEntity {
                    Name = "B",
                    Quantity = 1
                },
                new TrolleyProductQuantityEntity {
                    Name = "C",
                    Quantity = 1
                }
            };
            var trolleySpecialEntities = new List<TrolleySpecialEntity> { 
                new TrolleySpecialEntity
                {
                    Quantities = trolleySpecialProductQuantityEntitiesSpecials,
                    Total = 10
                } 
            };
            var trolleyEntities = new TrolleyEntity
            {
                Products = trolleyProductEntities,
                Specials = trolleySpecialEntities,
                Quantities = trolleySpecialProductQuantityEntities
            };

            var result = _sut.GetTrolleyTotal(trolleyEntities).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
