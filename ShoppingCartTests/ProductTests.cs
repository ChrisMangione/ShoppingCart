using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingCart.Controllers;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartTests
{
    [TestClass]
    public class ProductTests : TestServiceResolver
    {
        private readonly IProductRepository _productRepository;
        private readonly IExchangeRatesService _exchangeRateService;
        private readonly ILogger<ProductController> _logger;

        public ProductTests()
        {
            _productRepository = _serviceProvider.GetService<IProductRepository>() ?? Mock.Of<IProductRepository>();
            _exchangeRateService = _serviceProvider.GetService<IExchangeRatesService>() ?? Mock.Of<IExchangeRatesService>();
            _logger = Mock.Of<ILogger<ProductController>>();
        }

        [TestMethod]
        public void GetProducts_WithCurrency_ProductCountCorrect()
        {
            var currency = "AUD";
            var expectedResult = 4;

            var productController = new ProductController(_logger, _productRepository, _exchangeRateService);
            var result = productController.GetProducts(currency).Result.Count();

            Assert.IsTrue(result == expectedResult, $"Incorrect number of products returned. Expected: {expectedResult} Actual: {result}");
        }

        [TestMethod]
        public void GetProducts_WithCurrency_ProductCalculationCorrect()
        {
            var currency = "CNY";
            var rate = _exchangeRateService.GetExchangeRate(currency).Result;
            var expectedResult = 14.30M * rate;

            var productController = new ProductController(_logger, _productRepository, _exchangeRateService);
            var result = productController.GetProducts(currency).Result.First(o => o.Name == "Durian").Price;

            Assert.IsTrue(result == expectedResult, $"Product price calculation incorrect. Expected: {expectedResult} Actual: {result}");
        }
    }
}
