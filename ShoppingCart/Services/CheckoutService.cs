using ShoppingCart.Helpers;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Services
{
    public class CheckoutService : ICheckoutService
    {
        IProductRepository _productRepository { get; set; }

        public CheckoutService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<decimal> CalculateShipping(decimal totalCost, decimal exhangeRate)
        {
            var details = await _productRepository.GetShippingDetails();
            if (totalCost == 0)
                return 0;
            if (totalCost <= details.CutOff * exhangeRate)
                return details.LowerBound * exhangeRate;
            return details.UpperBound * exhangeRate;
        }

        public async Task<decimal> Checkout(int[] products, decimal exhangeRate)
        {
            var allProducts = await _productRepository.GetProducts(exhangeRate);
            var selectedProducts = products.Select(p => allProducts.First(a => a.ProductId == p));
            var productCost = selectedProducts.Sum(o => o.Price);
            var shipping = await CalculateShipping(selectedProducts.Sum(o => o.Price), exhangeRate);
            return productCost + shipping;
        }
    }
}
