using System;
using ChaosCMS;
using ChaosCMS.Controllers;
using ChaosCMS.Formatters;
using ChaosCMS.Managers;
using ChaosCMS.Rendering;
using ChaosCMS.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> for configuring Chaos services.
    /// </summary>
    public static class ChaosServiceCollectionExtensions
    {
        /// <summary>
        /// Adds and configures the chaos system for specific Page types
        /// </summary>
        /// <typeparam name="TPage">The type of the page.</typeparam>
        /// <typeparam name="TContent">the type of the content</typeparam>
        /// <typeparam name="TUser">The type of the user</typeparam>
        /// <typeparam name="TRole">The type of the Role</typeparam>
        /// <param name="service">The service collection</param>
        /// <returns>The instance of the <see cref="ChaosBuilder"/></returns>
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
        /// <typeparam name="TPage">The type of the page.</typeparam>
        /// <typeparam name="TContent">the type of the content</typeparam>
        /// <typeparam name="TUser">The type of the user</typeparam>
        /// <typeparam name="TRole">The type of the Role</typeparam>
        /// <param name="services">The service collection</param>
        /// <param name="options">The options to configure</param>
        /// <returns>The instance of the <see cref="ChaosBuilder"/></returns>
        public static ChaosBuilder AddChaos<TPage, TContent, TUser, TRole>(this IServiceCollection services, Action<ChaosOptions> options)
            where TPage : class, new()
            where TContent : class, new()
            where TUser : class, new()
            where TRole : class, new()
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var opt = new ChaosOptions();

            options?.Invoke(opt);

            var identityBuilder = services.AddIdentity<TUser, TRole>()
                .AddDefaultTokenProviders();

            services.AddSingleton(Options.Options.Create(opt.Security.Identity));

            var mvcBuilder = services.AddMvc(o =>
                {
                    o.OutputFormatters.Add(new JsonHalOutputFormatter(new[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json" }));
                })
                .AddRazorOptions(r =>
                {
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
                                typeof(ErrorController<TPage>),
                                typeof(ResourceController),
                                typeof(AccountController<TUser>),
                                typeof(UserController<TUser>),
                                typeof(AdminController)
                                ));

                }).AddControllersAsServices();

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