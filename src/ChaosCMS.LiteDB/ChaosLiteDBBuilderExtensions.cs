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
        public static IChaosBuilder AddLiteDBStores(this IChaosBuilder builder, Action<ChaosLiteDBStoreOptions> options = null)
        {
            builder.Services.TryAdd(GetDefaultServices(builder));

            if(options != null)
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
        public static IdentityBuilder AddLiteDBStores(this IdentityBuilder builder)
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

            services.AddScoped<ChaosLiteDBFactory>();

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
