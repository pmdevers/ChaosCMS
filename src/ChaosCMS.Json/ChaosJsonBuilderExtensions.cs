using ChaosCMS;
using ChaosCMS.Json.Stores;
using ChaosCMS.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="ChaosBuilder"/>
    /// </summary>
    public static class ChaosJsonBuilderExtensions
    {
        public static ChaosBuilder AddJsonStores(this ChaosBuilder builder)
        {
            builder.Services.TryAdd(GetDefaultServices(builder.PageType));
            return builder;
        }

        private static IServiceCollection GetDefaultServices(Type pageType)
        {
            var pageStoreType = typeof(PageStore<>).MakeGenericType(pageType);

            var services = new ServiceCollection();

            services.AddScoped(
                typeof(IPageStore<>).MakeGenericType(pageType),
                pageStoreType);

            return services;
        }
    }
}
