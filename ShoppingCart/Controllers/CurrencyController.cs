using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : Controller
    {
        private IExchangeRatesService _exchangeRatesService { get; set; }
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger, IExchangeRatesService exchangeRatesService)
        {
            _logger = logger;
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet]
        public async Task<IEnumerable<CurrencyModel>> GetCountries()
        {
            try
            {
                return await _exchangeRatesService.GetAvailableCountries();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetProducts");
                throw;
            }
        }
    }
}
