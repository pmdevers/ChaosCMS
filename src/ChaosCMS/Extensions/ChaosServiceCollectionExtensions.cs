using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;

using ChaosCMS;
using ChaosCMS.Managers;
using ChaosCMS.Razor;
using ChaosCMS.Controllers;
using System.IO;
using ChaosCMS.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json.Serialization;
using System.Linq;
using TypeInfo = System.Reflection.TypeInfo;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring Chaos services.
    /// </summary>
    public static class ChaosServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="service"></param>
        /// <returns></returns>
        public static ChaosBuilder AddChaos<TPage>(this IServiceCollection service)
            where TPage : class
        {
            return service.AddChaos<TPage>(options: null);
        }

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

            services.AddMvc()
                .AddJsonOptions(a=>a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .ConfigureApplicationPartManager(manager =>
                {
                    //manager.ApplicationParts.Clear();
                    manager.ApplicationParts.Add(
                        new TypesPart(
                                typeof(PageController<TPage>)
                            )
                        );
                })
                .AddControllersAsServices();

            services.AddRazor(razor => {
                razor.Root = Path.Combine(Directory.GetCurrentDirectory(), "templates");
            });

            services.TryAddScoped(typeof(IChaosHelper<>), typeof(ChaosHelper<>));

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

        private class TypesPart : ApplicationPart, IApplicationPartTypeProvider
        {
            public TypesPart(params Type[] types)
            {
                Types = types.Select(t => t.GetTypeInfo());
            }

            public override string Name => string.Join(", ", Types.Select(t => t.FullName));

            public IEnumerable<TypeInfo> Types { get; }
        }

    }
}
