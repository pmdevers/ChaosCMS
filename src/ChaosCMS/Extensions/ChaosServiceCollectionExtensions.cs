using System;
using System.Buffers;
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
using ChaosCMS.Formatters;
using ChaosCMS.Validators;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

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
        /// <typeparam name="TContent"></typeparam>
        /// <param name="service"></param>
        /// <returns></returns>
        public static ChaosBuilder AddChaos<TPage, TContent>(this IServiceCollection service)
            where TPage : class 
            where TContent : class
        {
            return service.AddChaos<TPage, TContent>(options: null);
        }

        /// <summary>
        /// Adds and configures the chaos system for specific Page types
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ChaosBuilder AddChaos<TPage, TContent>(this IServiceCollection services, Action<ChaosOptions> options)
            where TPage : class where TContent : class
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc(o =>
                {
                    o.OutputFormatters.Add(new JsonHalOutputFormatter(new[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json"}));  
                })
                .AddJsonOptions(a =>
                {
                    a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.ApplicationParts.Add(
                        new ChaosTypesPart(
                                typeof(PageController<TPage>),
                                typeof(ContentController<TContent>)
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
            services.TryAddScoped<IChaosContext<TPage>, ChaosContext<TPage>>();
            services.TryAddScoped<ChaosErrorDescriber>();
            services.TryAddScoped<PageManager<TPage>, PageManager<TPage>>();
            services.TryAddScoped<ContentManager<TContent>, ContentManager<TContent>>();

            // Validators
            services.TryAddScoped<IPageValidator<TPage>, DefaultPageValidator<TPage>>();
            services.TryAddScoped<IContentValidator<TContent>, DefaultContentValidator<TContent>>();

            if (options != null)
            {
                services.Configure(options);
            }

            var builder = new ChaosBuilder(typeof(TPage), typeof(TContent), services);

            services.TryAddSingleton(builder);

            return builder;
        }
    }
}
