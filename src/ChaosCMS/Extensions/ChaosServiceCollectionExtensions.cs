using System;
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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

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
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="service"></param>
        /// <returns></returns>
        public static ChaosBuilder AddChaos<TPage, TContent, TUser, TRole>(this IServiceCollection service)
            where TPage : class, new()
            where TContent : class, new()
            where TUser : class, new()
            where TRole : class, new()
        {
            return service.AddChaos<TPage, TContent, TUser, TRole>(options: null);
        }

        /// <summary>
        /// Adds and configures the chaos system for specific Page types
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TContent"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ChaosBuilder AddChaos<TPage, TContent, TUser, TRole>(this IServiceCollection services, Action<ChaosOptions> options)
            where TPage : class , new()
            where TContent : class, new()
            where TUser : class, new()
            where TRole : class, new()
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var opt = new ChaosOptions();

            options?.Invoke(opt);

            var identityBuilder = services.AddIdentity<TUser, TRole>();

            services.AddSingleton<IOptions<IdentityOptions>>(Options.Options.Create(opt.Security));

            var mvcBuilder = services.AddMvc(o =>
                {
                    o.OutputFormatters.Add(new JsonHalOutputFormatter(new[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json"}));
                })
                .AddRazorOptions(r=> {
                    r.ViewLocationExpanders.Add(new ChaosViewLocationRemapper());
                    r.FileProviders.Add(new ChaosFileProvider(new ResourceManager()));
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
                                typeof(RenderController<TPage>),
                                typeof(ResourceController),
                                typeof(AccountController<TUser>),
                                typeof(AdminController)
                            )
                        );
                })
                .AddControllersAsServices();

            services.TryAddScoped<IChaos, DefaultChaosService<TPage, TContent>>();

            // Chaos services
            services.TryAddSingleton<ChaosMarkerService>();
            services.TryAddScoped<ChaosErrorDescriber>();
            services.TryAddScoped<PageManager<TPage>, PageManager<TPage>>();
            services.TryAddScoped<ContentManager<TContent>, ContentManager<TContent>>();
            services.TryAddScoped<ResourceManager, ResourceManager>();

            // Validators
            services.TryAddScoped<IPageValidator<TPage>, DefaultPageValidator<TPage>>();
            services.TryAddScoped<IContentValidator<TContent>, DefaultContentValidator<TContent>>();

            services.AddSingleton<IRenderer<TContent>, DefaultRenderer<TContent>>();
            services.AddSingleton<IRenderer<TContent>, HtmlRenderer<TContent>>();
            services.AddSingleton<IRenderer<TContent>, StringRenderer<TContent>>();
            services.AddSingleton<IRenderer<TContent>, MacroRenderer<TContent>>();
            services.AddSingleton<IRenderer<TContent>, CarouselRenderer<TContent>>();
            services.AddSingleton<IRenderer<TContent>, LinkContentRenderer<TContent>>();

            if (options != null)
            {
                services.Configure(options);
            }

            var builder = new ChaosBuilder(typeof(TPage), typeof(TContent), identityBuilder, mvcBuilder, services);

            services.TryAddSingleton(builder);

            return builder;
        }
    }
}
