using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WooliesX.Data.Entities;
using WooliesX.Data.Entities.Trolley;
using WooliesX.Data.Enums;
using WooliesX.Http;

namespace WooliesX.Data.UnitTests
{
    [TestClass]
    public class TrolleyTotalProcessorTests
    {
        private Mock<IHttpClientHelper> _mockHttpClientHelper;
        private ITrolleyTotalProcessor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpClientHelper = new Mock<IHttpClientHelper>(MockBehavior.Strict);
            _sut = new TrolleyTotalProcessor(_mockHttpClientHelper.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHttpClientHelper.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenHttpClientHelper_IsNull_ThrowsException()
        {
            _ = new TrolleyTotalProcessor(null);
        }

        [TestMethod]
        public void GetTrolleyTotal_ReturnsTrolleyTotal()
        {
            var expected = "10";

            _mockHttpClientHelper.Setup(s => s.PostAsync<TrolleyEntity, string>(Constants.TROLLEY_CALCULATOR, It.IsAny<TrolleyEntity>())).ReturnsAsync(expected);
            var result = _sut.GetTrolleyTotal(It.IsAny<TrolleyEntity>()).Result;
            Assert.AreEqual(expected, result);
        }
    }
}
