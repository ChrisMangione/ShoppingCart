using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private ICheckoutService _checkoutService;
        private IExchangeRatesService _exchangeRatesService { get; set; }
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger, ICheckoutService checkoutService, IExchangeRatesService exchangeRatesService)
        {
            _logger = logger;
            _checkoutService = checkoutService;
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet]
        public async Task<decimal> CalculateShipping(decimal totalCost, string currency)
        {
            try
            {
                decimal exhangeRate = await _exchangeRatesService.GetExchangeRate(currency);
                return await _checkoutService.CalculateShipping(totalCost, exhangeRate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Shipping calculation error");
                throw;
            }
        }

        [HttpPost]
        public async Task<decimal> Checkout([FromBody] CheckoutDTO checkoutObject)
        {
            try
            {
                decimal exhangeRate = await _exchangeRatesService.GetExchangeRate(checkoutObject.Currency);
                return await _checkoutService.Checkout(checkoutObject.Products, exhangeRate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Checkout error");
                throw;
            }
        }
    }
}
