using ShoppingCart.Models;

namespace ShoppingCart.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductModel>> GetProducts(decimal exchangeRate);
        public Task<ShippingDetailModel> GetShippingDetails();
    }
}