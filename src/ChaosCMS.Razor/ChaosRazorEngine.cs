using System;
using System.Dynamic;
using System.IO;
using ChaosCMS.Razor.Rendering;
using ChaosCMS.Razor.Templating;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace ChaosCMS.Razor
{
	public class ChaosRazorEngine : IChaosRazorEngine
	{
		
		private readonly IPageLookup pageLookup;
        private readonly IActivator activator;
        private RazorConfiguration configuration;

        public ChaosRazorEngine(IEngineCore core, IPageLookup pagelookup, IActivator activator, IOptions<RazorConfiguration> options)
		{
			if (core == null)
			{
				throw new ArgumentNullException(nameof(core));
			}

			if (pagelookup == null)
			{
				throw new ArgumentNullException();
			}

			this.Core = core;
			this.pageLookup = pagelookup;
            this.activator = activator;
            this.configuration = options.Value;
		}

        public IEngineCore Core { get; }

        /// <summary>
        /// Parses a template with a given <paramref name="key" />
        /// </summary>
        /// <typeparam name="T">Type of the Model</typeparam>
        /// <param name="key">Key used to resolve a template</param>
        /// <param name="model">Template model</param>
        /// <returns>Returns parsed string</returns>
        public string Parse<T>(string key, T model)
		{
			return Parse(key, model, typeof(T), viewBag: null);
		}

		/// <summary>
		/// Parses a template with a given <paramref name="key" /> and viewBag
		/// </summary>
		/// <param name="key">Key used to resolve a template</param>
		/// <param name="model">Template model</param>
		/// <param name="viewBag">Dynamic ViewBag (can be null)</param>
		/// <returns>Returns parsed string</returns>
		/// <remarks>Result is stored in cache</remarks>
		public string Parse<T>(string key, T model, ExpandoObject viewBag)
		{
			return Parse(key, model, typeof(T), viewBag);
		}

		/// <summary>
		/// Parses a template with a given <paramref name="key" />
		/// </summary>
		/// <param name="key">Key used to resolve a template</param>
		/// <param name="model">Template model</param>
		/// <param name="modelType">Type of the model</param>
		/// <param name="viewBag">Dynamic ViewBag (can be null)</param>
		/// <returns>Returns parsed string</returns>
		/// <remarks>Result is stored in cache</remarks>
		public string Parse(string key, object model, Type modelType, ExpandoObject viewBag)
		{
			PageLookupResult result = pageLookup.GetPage(key);

			if (!result.Success)
			{
				throw new ChaosRazorException($"Can't find a view with a specified key ({key})");
			}

			var pageContext = new PageContext(viewBag) { ModelTypeInfo = new ModelTypeInfo(modelType) };
			foreach (var viewStartPage in result.ViewStartEntries)
			{
				pageContext.ViewStartPages.Add(viewStartPage.PageFactory());
			}

			TemplatePage page = result.ViewEntry.PageFactory();
			page.PageContext = pageContext;

			return RunTemplate(page, model);
		}

		/// <summary>
		/// Creates an instance of the compiled type and casts it to TemplatePage
		/// </summary>
		/// <param name="compiledType">Type to activate</param>
		/// <returns>Template page</returns>
		public TemplatePage Activate(Type compiledType)
		{
			return (TemplatePage)activator.CreateInstance(compiledType);
		}

		/// <summary>
		/// Runs a template, renders a Layout pages and sections.
		/// </summary>
		/// <param name="page">Page to run</param>
		/// <param name="model">Mode of the page</param>
		public string RunTemplate(TemplatePage page, object model)
		{
			object pageModel = page.PageContext.ModelTypeInfo.CreateTemplateModel(model);
			page.SetModel(pageModel);
			page.Path = page.PageContext.ExecutingFilePath;

			using (var writer = new StringWriter())
			{
				page.PageContext.Writer = writer;

				using (var renderer = new PageRenderer(page, pageLookup))
				{
					renderer.ViewStartPages.AddRange(page.PageContext.ViewStartPages);
					renderer.PreRenderCallbacks.AddRange(configuration.PreRenderCallbacks);
					renderer.RenderAsync(page.PageContext).GetAwaiter().GetResult();
					return writer.ToString();
				}
			}
		}
	}
}
