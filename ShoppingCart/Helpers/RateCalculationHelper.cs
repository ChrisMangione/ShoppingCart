using ShoppingCart.Models;

namespace ShoppingCart.Helpers
{
    public class RateCalculationHelper
    {
        public static ProductModel ApplyRate(ProductModel model, decimal exchangeRate)
        {
            model.Price *= exchangeRate;
            return model;
        }
    }
}
