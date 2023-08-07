using Adapter.MercadoBitcoinAdapter;
using Domain.Adapters;
using MercadoBitcoinAdapter.Clients;
using Refit;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection.MercadoBitcoinAdapter
{
    public static class MercadoBitcoinAdapterServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddMercadoBitcoinAdapter(
            this IServiceCollection services,
            MercadoBitcoinAdapterConfiguration mercadoBitcoinConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (mercadoBitcoinConfiguration == null)
            {
                throw new ArgumentNullException(nameof(mercadoBitcoinConfiguration));
            }

            services.AddSingleton(mercadoBitcoinConfiguration);

            services.AddScoped(serviceProvider =>
            {
                var httpClient = new HttpClient();

                httpClient.BaseAddress =
                    new Uri(mercadoBitcoinConfiguration.MercadoBitcoinApiUrlBase);

                return RestService.For<IMercadoBitcoinApi>(httpClient);
            });

            services.AddScoped<IMercadoBitcoinAdapter, 
                Adapter.MercadoBitcoinAdapter.MercadoBitcoinAdapter>();

            return services;
        }
    }
}
