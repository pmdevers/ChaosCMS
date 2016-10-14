using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ChaosCMS;
using ChaosCMS.Managers;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring Chaos services.
    /// </summary>
    public static class ChaosServiceCollectionExtensions
    {
        /// <summary>
        /// Adds and configures the chaos system for specific Page types
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ChaosBuilder AddChaos<TPage>(this IServiceCollection services, Action<ChaosOptions> options)
            where TPage : class
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Chaos services
            services.TryAddSingleton<ChaosMarkerService>();
            services.TryAddScoped<ChaosErrorDescriber>();
            services.TryAddScoped<PageManager<TPage>, PageManager<TPage>>();
                       
            if(options != null)
            {
                services.Configure(options);
            }

            var builder = new ChaosBuilder(typeof(TPage), services);

            services.TryAddSingleton(builder);

            return builder;
        }


    }
}
