using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Interfaces;
using ShoppingCart.Services;
using System;

namespace ShoppingCartTests
{
    public class TestServiceResolver
    {
        protected IServiceCollection _serviceCollection { get; set; }
        protected IServiceProvider _serviceProvider { get; set; }

        public TestServiceResolver()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton<IProductRepository, MockProductRepository>();
            _serviceCollection.AddSingleton<IExchangeRatesService, MockExchangeRatesService>();
            _serviceCollection.AddTransient<ICheckoutService, CheckoutService>();
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
    }
}
