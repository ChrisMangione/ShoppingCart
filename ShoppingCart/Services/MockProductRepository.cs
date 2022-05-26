using ShoppingCart.Helpers;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Services
{
    public class MockProductRepository : IProductRepository
    {
        private static IEnumerable<ProductModel> _products => new List<ProductModel>
        {
            new ProductModel
            {
                ProductId = 1,
                Name = $"Mango",
                Description = $"Ripe and Juicy",
                Price = 8M
            },
            new ProductModel
            {
                ProductId = 2,
                Name = $"Durian",
                Description = $"test description 1",
                Price = 14.30M
            },
            new ProductModel
            {
                ProductId = 3,
                Name = $"Banana",
                Description = $"test description 2",
                Price = 3.20M
            },
            new ProductModel
            {
                ProductId = 4,
                Name = $"Apple",
                Description = $"Hard and crunchy",
                Price = 5M
            }
        };

        public async Task<IEnumerable<ProductModel>> GetProducts(decimal exchangeRate)
        {
            return await Task.Run(() => new List<ProductModel>(_products.Select(product => RateCalculationHelper.ApplyRate(product, exchangeRate))));
        }

        public async Task<ShippingDetailModel> GetShippingDetails()
        {
            return await Task.Run(() => new ShippingDetailModel { CutOff = 50, LowerBound = 10, UpperBound = 20 });
        }
    }
}
