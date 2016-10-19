using ChaosCMS.Razor.Caching;
using ChaosCMS.Razor.Compilation;
using ChaosCMS.Razor.Templating;
using ChaosCMS.Razor.Templating.FileSystem;
using ChaosCMS.Razor.Host;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using ChaosCMS.Razor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RazorServiceCollectionExtensions
    {
        public static IServiceCollection AddRazor(this IServiceCollection services, Action<RazorConfiguration> options)
        {

            services.TryAddSingleton<IChaosRazorEngine, ChaosRazorEngine>();
            services.TryAddSingleton<IRazorTemplateCompiler, DefaultRazorTemplateCompiler>();
            services.TryAddSingleton<IMetadataResolver, UseEntryAssemblyMetadataResolver>();
            services.TryAddSingleton<ICompilerService, RoslynCompilerService>();
            services.TryAddSingleton<IEngineCore, EngineCore>();
            services.TryAddSingleton<ITemplateManager, FilesystemTemplateManager>();
            services.TryAddSingleton<IPageLookup, FilesystemPageLookup>();
            services.TryAddSingleton<ICompilerCache, TrackingCompilerCache>();
            services.TryAdd(ServiceDescriptor.Singleton<IPageFactoryProvider>(serviceProvider => {
                var core = serviceProvider.GetRequiredService<IEngineCore>();
                var compilerCache = serviceProvider.GetRequiredService<ICompilerCache>();
                return new CachingPageFactory(core.KeyCompile, compilerCache);
            }));
            services.TryAddSingleton<IPageLookup, FilesystemPageLookup>();
            services.TryAddSingleton<IRazorTemplateCompiler, DefaultRazorTemplateCompiler>();


            services.TryAddSingleton<ChaosRazorHost>();
            services.TryAddSingleton<IActivator, DefaultActivator>();
            
            services.Configure(options);
            
            return services;
        }
    }
}
