using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        private IExchangeRatesService _exchangeRatesService { get; set; }
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IProductRepository repository, IExchangeRatesService exchangeRatesService)
        {
            _logger = logger;
            _repository = repository;
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts(string currency = "AUD")
        {
            try
            {
                decimal exhangeRate = await _exchangeRatesService.GetExchangeRate(currency);
                var products = await _repository.GetProducts(exhangeRate);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetProducts");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
