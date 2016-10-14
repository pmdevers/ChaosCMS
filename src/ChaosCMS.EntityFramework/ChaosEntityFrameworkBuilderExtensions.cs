using ChaosCMS;
using ChaosCMS.EntityFramework;
using ChaosCMS.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="ChaosBuilder"/> for adding entity framework stores.
    /// </summary>
    public static class ChaosEntityFrameworkBuilderExtensions
    {
        /// <summary>
        /// Adds an Entity Framework implementation of chaos information stores.
        /// </summary>
        /// <typeparam name="TContext">The Entity Framework database context to use.</typeparam>
        /// <param name="builder">th <see cref="ChaosBuilder"/> instance this method extends.</param>
        /// <returns>The <see cref="ChaosBuilder"/> instance this method extends.</returns>
        public static ChaosBuilder AddEntityFrameworkStores<TContext>(this ChaosBuilder builder)
            where TContext : DbContext
        {
            builder.Services.TryAdd(GetDefaultServices(builder.PageType, typeof(TContext)));
            return builder;
        }

        /// <summary>
        /// Adds an Entity Framework implementation of chaos information stores.
        /// </summary>
        /// <typeparam name="TContext">The Entity Framework database context to use.</typeparam>
        /// <typeparam name="TKey">The type of the primary key.</typeparam>
        /// <param name="builder">th <see cref="ChaosBuilder"/> instance this method extends.</param>
        /// <returns>The <see cref="ChaosBuilder"/> instance this method extends.</returns>
        public static ChaosBuilder AddEntityFrameworkStores<TContext, TKey>(this ChaosBuilder builder)
            where TContext : DbContext
            where TKey : IEquatable<TKey>
        {
            builder.Services.TryAdd(GetDefaultServices(builder.PageType, typeof(TContext), typeof(TKey)));
            return builder;
        }

        private static IServiceCollection GetDefaultServices(Type pageType, Type contextType, Type keyType = null)
        {
            Type pageStoreType;
            keyType = keyType ?? typeof(string);
            pageStoreType = typeof(PageStore<,,>).MakeGenericType(pageType, contextType, keyType);


            var services = new ServiceCollection();
            services.AddScoped(
                typeof(IPageStore<>).MakeGenericType(pageType),
                pageStoreType);

            return services;
        }
    }
}
