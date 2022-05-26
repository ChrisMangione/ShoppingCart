namespace ShoppingCart.Interfaces
{
    public interface ICheckoutService
    {
        public Task<decimal> CalculateShipping(decimal totalCost, decimal exhangeRate);
        public Task<decimal> Checkout(int[] products, decimal exchangeRate);
    }
}
