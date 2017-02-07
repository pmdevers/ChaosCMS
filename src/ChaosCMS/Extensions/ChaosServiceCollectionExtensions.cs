﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ChaosCMS;
using ChaosCMS.Managers;
using ChaosCMS.Controllers;
using Newtonsoft.Json.Serialization;
using ChaosCMS.Formatters;
using ChaosCMS.Validators;
using Microsoft.AspNetCore.Mvc.Razor;
using ChaosCMS.Rendering;

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

            var opt = new ChaosOptions();

            options?.Invoke(opt);

            services.AddMvc(o =>
                {
                    o.OutputFormatters.Add(new JsonHalOutputFormatter(new[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json"}));
                })
                .AddRazorOptions(r=> {
                    r.ViewLocationExpanders.Add(new ChaosViewLocationRemapper());
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
                                typeof(ContentController<TContent>),
                                typeof(RenderController<TPage>)
                            )
                        );
                })
                .AddControllersAsServices();

            services.AddTransient<IChaos, DefaultChaosService<TPage, TContent>>();

            // Chaos services
            services.TryAddSingleton<ChaosMarkerService>();
            services.TryAddScoped<ChaosErrorDescriber>();
            services.TryAddScoped<PageManager<TPage>, PageManager<TPage>>();
            services.TryAddScoped<ContentManager<TContent>, ContentManager<TContent>>();

            // Validators
            services.TryAddScoped<IPageValidator<TPage>, DefaultPageValidator<TPage>>();
            services.TryAddScoped<IContentValidator<TContent>, DefaultContentValidator<TContent>>();

            services.AddSingleton<IRenderer<TContent>, HtmlRenderer<TContent>>();
            services.AddSingleton<IRenderer<TContent>, StringRenderer<TContent>>();

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
