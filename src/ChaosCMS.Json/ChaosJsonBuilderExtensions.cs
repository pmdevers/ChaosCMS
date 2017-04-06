using ChaosCMS;
using ChaosCMS.Json.Stores;
using ChaosCMS.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using ChaosCMS.Json;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.TryAdd(GetDefaultServices(builder));

            if (options != null)
            {
                builder.Services.Configure(options);
            }

            return builder;
        }


        private static IServiceCollection GetDefaultServices(ChaosBuilder builder)
        {
            var pageStoreType = typeof(PageStore<>).MakeGenericType(builder.PageType);
            var contentStoreType = typeof(ContentStore<>).MakeGenericType(builder.ContentType);
            var userStoreType = typeof(UserStore<>).MakeGenericType(builder.IdentityBuilder.UserType);
            var roleStoreType = typeof(RoleStore<>).MakeGenericType(builder.IdentityBuilder.RoleType);

            var services = new ServiceCollection();

            services.AddScoped(
                typeof(IPageStore<>).MakeGenericType(builder.PageType),
                pageStoreType);

            services.AddScoped(
                typeof(IContentStore<>).MakeGenericType(builder.ContentType),
                contentStoreType);

            services.AddScoped(
                typeof(IUserStore<>).MakeGenericType(builder.IdentityBuilder.UserType), 
                userStoreType);

            services.AddScoped(
                typeof(IRoleStore<>).MakeGenericType(builder.IdentityBuilder.RoleType),
                roleStoreType);

            return services;
        }
    }
}
