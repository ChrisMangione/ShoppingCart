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
        public async Task<ActionResult<decimal>> CalculateShipping(decimal totalCost, string currency)
        {
            try
            {
                decimal exhangeRate = await _exchangeRatesService.GetExchangeRate(currency);
                var shipping = await _checkoutService.CalculateShipping(totalCost, exhangeRate);
                return Ok(shipping);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Shipping calculation error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<decimal>> Checkout([FromBody] CheckoutDTO checkoutObject)
        {
            try
            {
                decimal exhangeRate = await _exchangeRatesService.GetExchangeRate(checkoutObject.Currency);
                var total = await _checkoutService.Checkout(checkoutObject.Products, exhangeRate);
                return Ok(total);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Checkout error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
