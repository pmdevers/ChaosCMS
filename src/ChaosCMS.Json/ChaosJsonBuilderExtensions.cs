using ChaosCMS;
using ChaosCMS.Json.Stores;
using ChaosCMS.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ChaosCMS.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="ChaosBuilder"/>
    /// </summary>
    public static class ChaosJsonBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ChaosBuilder AddJsonStores(this ChaosBuilder builder, Action<ChaosJsonStoreOptions> options = null)
        {
            builder.Services.TryAdd(GetDefaultServices(builder.PageType, builder.ContentType));

            if (options != null)
            {
                builder.Services.Configure(options);
            }

            return builder;
        }


        private static IServiceCollection GetDefaultServices(Type pageType, Type contentType)
        {
            var pageStoreType = typeof(PageStore<>).MakeGenericType(pageType);

            var services = new ServiceCollection();

            services.AddScoped(
                typeof(IPageStore<>).MakeGenericType(pageType),
                pageStoreType);

            services.AddScoped(
                typeof(IContentStore<>).MakeGenericType(contentType),
                pageStoreType);

            return services;
        }
    }
}
