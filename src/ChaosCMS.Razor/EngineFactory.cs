using System;
using ChaosCMS.Razor.Caching;
using ChaosCMS.Razor.Templating;
using ChaosCMS.Razor.Templating.Embedded;
using ChaosCMS.Razor.Templating.FileSystem;

namespace ChaosCMS.Razor
{
	public static class EngineFactory
	{
		/// <summary>
		/// Creates a <see cref="ChaosRazorEngine"/> that resolves templates by searching 
		/// them on physical storage with a given <see cref="IEngineConfiguration"/>
		/// and tracks file changes with <seealso cref="System.IO.FileSystemWatcher"/>
		/// </summary>
		/// <param name="root">Root folder where views are stored</param>
		/// <param name="configuration">Engine configuration</param>
		public static ChaosRazorEngine CreatePhysical(string root, Action<IEngineConfiguration> options = null)
		{
			if (string.IsNullOrEmpty(root))
			{
				throw new ArgumentNullException(nameof(root));
			}

            var configuration = new EngineConfiguration();
            options?.Invoke(configuration);

			ITemplateManager templateManager = new FilesystemTemplateManager(root);
			IEngineCore core = new EngineCore(templateManager, configuration);

			ICompilerCache compilerCache = new TrackingCompilerCache(root);
			IPageFactoryProvider pageFactory = new CachingPageFactory(core.KeyCompile, compilerCache);
			IPageLookup pageLookup = new FilesystemPageLookup(pageFactory);

			return new ChaosRazorEngine(core, pageLookup);
		}

		/// <summary>
		/// Creates a <see cref="ChaosRazorEngine"/> that resolves templates inside given type assembly as a EmbeddedResource
		/// with a given <see cref="IEngineConfiguration"/>
		/// </summary>
		/// <param name="rootType">Root type where resource is located</param>
		/// <param name="configuration">Engine configuration</param>
		public static ChaosRazorEngine CreateEmbedded(Type rootType, Action<IEngineConfiguration> config = null)
		{
            ITemplateManager manager = new EmbeddedResourceTemplateManager(rootType);
			var dependencies = CreateDefaultDependencies(manager, config);

			return new ChaosRazorEngine(dependencies.Item1, dependencies.Item2);
		}

		private static Tuple<IEngineCore, IPageLookup> CreateDefaultDependencies(
			ITemplateManager manager,
			Action<IEngineConfiguration> config = null)
		{
            var configuration = new EngineConfiguration();
            config?.Invoke(configuration);
            IEngineCore core = new EngineCore(manager, configuration);

			IPageFactoryProvider pageFactory = new DefaultPageFactory(core.KeyCompile);
			IPageLookup lookup = new DefaultPageLookup(pageFactory);

			return new Tuple<IEngineCore, IPageLookup>(core, lookup);
		}
	}
}
