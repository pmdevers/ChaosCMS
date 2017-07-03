using System;
using ChaosCMS;
using ChaosCMS.Json;
using ChaosCMS.Json.Stores;
using ChaosCMS.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
        public static IChaosBuilder AddJsonStores(this IChaosBuilder builder, Action<ChaosJsonStoreOptions> options = null)
        {
            builder.Services.TryAdd(GetDefaultServices(builder));

            if (options != null)
            {
                builder.Services.Configure(options);
            }

            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IdentityBuilder AddJsonStores(this IdentityBuilder builder)
        {
            var userStoreType = typeof(UserStore<>).MakeGenericType(builder.UserType);
            var roleStoreType = typeof(RoleStore<>).MakeGenericType(builder.RoleType);

            builder.Services.AddScoped(
                typeof(IUserStore<>).MakeGenericType(builder.UserType),
                userStoreType);

            builder.Services.AddScoped(
                typeof(IRoleStore<>).MakeGenericType(builder.RoleType),
                roleStoreType);

            return builder;
        }

        private static IServiceCollection GetDefaultServices(IChaosBuilder builder)
        {
            var pageStoreType = typeof(PageStore<>).MakeGenericType(builder.PageType);
            var pageTypeStoreType = typeof(PageTypeStore<>).MakeGenericType(builder.PageTypeType);
            

            var services = new ServiceCollection();

            services.AddScoped(
                typeof(IPageStore<>).MakeGenericType(builder.PageType),
                pageStoreType);

            services.AddScoped(
                typeof(IPageTypeStore<>).MakeGenericType(builder.PageTypeType),
                pageTypeStoreType);

            

            return services;
        }
    }
}