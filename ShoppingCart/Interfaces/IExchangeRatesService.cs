using ShoppingCart.Models;

namespace ShoppingCart.Interfaces
{
    public interface IExchangeRatesService
    {
        public Task<decimal> GetExchangeRate(string currency);
        public Task<List<CurrencyModel>> GetAvailableCountries();
    }
}
