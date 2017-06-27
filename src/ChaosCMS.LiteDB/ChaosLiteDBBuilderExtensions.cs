using ChaosCMS;
using ChaosCMS.LiteDB;
using ChaosCMS.LiteDB.Stores;
using ChaosCMS.Stores;
using LiteDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ChaosLiteDBBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ChaosBuilder AddLiteDBStores(this ChaosBuilder builder, Action<ChaosLiteDBStoreOptions> options = null)
        {
            builder.Services.TryAdd(GetDefaultServices(builder));

            if(options != null)
            {
                builder.Services.Configure(options);
            }

            return builder;
        }

        private static IServiceCollection GetDefaultServices(ChaosBuilder builder)
        {
            var pageStoreType = typeof(PageStore<>).MakeGenericType(builder.PageType);
            var pageTypeStoreType = typeof(PageTypeStore<>).MakeGenericType(builder.PageTypeType);
            var contentStoreType = typeof(ContentStore<>).MakeGenericType(builder.ContentType);
            var userStoreType = typeof(UserStore<>).MakeGenericType(builder.IdentityBuilder.UserType);
            var roleStoreType = typeof(RoleStore<>).MakeGenericType(builder.IdentityBuilder.RoleType);

            var services = new ServiceCollection();

            services.AddScoped(
                typeof(IPageStore<>).MakeGenericType(builder.PageType),
                pageStoreType);

            services.AddScoped(
                typeof(IPageTypeStore<>).MakeGenericType(builder.PageTypeType),
                pageTypeStoreType);

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
