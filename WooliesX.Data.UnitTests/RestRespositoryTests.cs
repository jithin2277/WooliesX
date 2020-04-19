using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.Data.Repositories;
using WooliesX.Http;

namespace WooliesX.Data.UnitTests
{
    [TestClass]
    public class RestRespositoryTests
    {
        private Mock<IHttpClientHelper> _mockHttpClientHelper;
        private IRepository<FooBar> _sut;
        private string _apiUrl = "http://www.google.com";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);
            _sut = new RestRespository<FooBar>(_apiUrl, _mockHttpClientHelper.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHttpClientHelper.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenParametersAreNull_ThrowsException()
        {
            _ = new RestRespository<FooBar>(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenApiUrlIsNull_ThrowsException()
        {
            _ = new RestRespository<FooBar>(null, _mockHttpClientHelper.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenHttpClientHelperIsNull_ThrowsException()
        {
            _ = new RestRespository<FooBar>(_apiUrl, null);
        }

        [TestMethod]
        public void GetAll_ReturnsApiResult()
        {
            var expected = new List<FooBar> { new FooBar { Foo = "foo", Bar = "bar" } };

            _mockHttpClientHelper.Setup(s => s.GetAsync<IEnumerable<FooBar>>(_apiUrl)).ReturnsAsync(expected);

            var result = _sut.GetAll().Result.ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }
        }
    }
}
