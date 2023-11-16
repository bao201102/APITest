using APITest.Application.Services.Implements;
using APITest.Application.Services.Interfaces;
using APITest.Application.Utilities;
using APITest.Domain.Interfaces;
using System.Reflection;

namespace APITest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
