using ShoppingCart.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class MockExchangeRatesService : IExchangeRatesService
    {
        private List<CurrencyModel> _exchangeRates => new List<CurrencyModel>()
        {
            new CurrencyModel { CurrencyCode = "AUD", Rate = 1, Country = "Australia" },
            new CurrencyModel { CurrencyCode = "EUR", Rate = 0.6M, Country = "Europe" },
            new CurrencyModel { CurrencyCode = "USD", Rate = 0.7M, Country = "USA" },
            new CurrencyModel { CurrencyCode = "JPY", Rate = 88.98M, Country = "Japan" },
            new CurrencyModel { CurrencyCode = "CNY", Rate = 4.71M, Country = "China" },
        };

        public async Task<decimal> GetExchangeRate(string currency)
        {
            return await Task.Run(() => 
                _exchangeRates.FirstOrDefault(model => 
                    model.CurrencyCode == currency, new CurrencyModel { Rate = 1}).Rate);
        }

        public async Task<List<CurrencyModel>> GetAvailableCountries()
        {
            return await Task.Run(() => _exchangeRates);
        }
    }
}
