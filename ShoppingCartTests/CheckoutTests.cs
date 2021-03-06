using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingCart.Controllers;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System.Threading.Tasks;

namespace ShoppingCartTests
{
    [TestClass]
    public class CheckoutTests : TestServiceResolver
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IExchangeRatesService _exchangeRateService;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutTests()
        {
            _checkoutService = _serviceProvider.GetService<ICheckoutService>() ?? Mock.Of<ICheckoutService>();
            _exchangeRateService = _serviceProvider.GetService<IExchangeRatesService>() ?? Mock.Of<IExchangeRatesService>();
            _logger = Mock.Of<ILogger<CheckoutController>>();
        }


        [TestMethod]
        [DataRow("AUD")]
        [DataRow("CNY")]
        [DataRow("USD")]
        public async Task CalculateShipping_LowerBound_CalculationCorrect(string currency)
        {
            var rate = _exchangeRateService.GetExchangeRate(currency).Result;
            var totalCost = 25M * rate;
            var expectedResult = 10;

            var checkoutController = new CheckoutController(_logger, _checkoutService, _exchangeRateService);
            var actionResult = await checkoutController.CalculateShipping(totalCost, currency);
            var result = actionResult.GetActionResult();

            Assert.IsTrue(result == expectedResult * rate, $"Lower Bound shipping calculation error with currency: {currency} ");
        }

        [TestMethod]
        [DataRow("AUD")]
        [DataRow("CNY")]
        [DataRow("USD")]
        public async Task CalculateShipping_UpperBound_CalculationCorrect(string currency)
        {
            var rate = _exchangeRateService.GetExchangeRate(currency).Result;
            var totalCost = 51M * rate;
            var expectedResult = 20;

            var checkoutController = new CheckoutController(_logger, _checkoutService, _exchangeRateService);
            var actionResult = await checkoutController.CalculateShipping(totalCost, currency);
            var result = actionResult.GetActionResult();

            Assert.IsTrue(result == expectedResult * rate, $"Upper Bound shipping calculation error with currency: {currency} ");
        }

        [TestMethod]
        public async Task Checkout_WithProduct_ExpectedResult()
        {
            var dto = new CheckoutDTO
            {
                Currency = "AUD",
                Products = new[] { 1 },
            };
            
            var expectedResult = 18M;

            var checkoutController = new CheckoutController(_logger, _checkoutService, _exchangeRateService);
            var actionResult = await checkoutController.Checkout(dto);
            var result = actionResult.GetActionResult();

            Assert.IsTrue(result == expectedResult, $"Checkout Calculation error. Exepected:{expectedResult} Actual:{result}");
        }

        [TestMethod]
        public async Task Checkout_WithProducts_ExpectedResult()
        {
            var dto = new CheckoutDTO
            {
                Currency = "AUD",
                Products = new[] { 1,2,3,4,1,2,3,4 },
            };

            var expectedResult = 81M;

            var checkoutController = new CheckoutController(_logger, _checkoutService, _exchangeRateService);
            var actionResult = await checkoutController.Checkout(dto);
            var result = actionResult.GetActionResult();

            Assert.IsTrue(result == expectedResult, $"Checkout Calculation error. Exepected:{expectedResult} Actual:{result}");
        }
    }
}