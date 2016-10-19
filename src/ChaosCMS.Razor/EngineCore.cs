using System;
using ChaosCMS.Razor.Caching;
using ChaosCMS.Razor.Compilation;
using ChaosCMS.Razor.Host;
using ChaosCMS.Razor.Templating;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace ChaosCMS.Razor
{
	public class EngineCore : IEngineCore
	{
		/// <summary>
		/// Creates <see cref="EngineCore" /> with specified <seealso cref="EngineConfiguration"/>/>
		/// </summary>
		/// <param name="templateManager">Template manager</param>
		/// <param name="configuration">Engine configuration options</param>
		public EngineCore(
			ITemplateManager templateManager,
            IRazorTemplateCompiler templateCompiler,
            ICompilerService compilerservice,
            IActivator activator,
            IOptions<RazorConfiguration> configuration)
		{
			this.templateCompiler = templateCompiler;
            this.compilerService = compilerservice;
			this.templateManager = templateManager;
            this.activator = activator;
            this.configuration = configuration.Value;
		}

        private readonly ITemplateManager templateManager;
        private readonly IRazorTemplateCompiler templateCompiler;
        private readonly ICompilerService compilerService;
        private readonly IActivator activator;
        private readonly RazorConfiguration configuration;

        /// <summary>
        /// Generates razor template by parsing given <param name="templateSource" />
        /// </summary>
        /// <param name="templateSource"></param>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public string GenerateRazorTemplate(ITemplateSource templateSource, ModelTypeInfo modelTypeInfo)
		{
			var host = new ChaosRazorHost(null);

			if (modelTypeInfo != null)
			{
				host.DefaultModel = modelTypeInfo.TemplateTypeName;
			}

			return templateCompiler.CompileTemplate(host, templateSource);
		}

		/// <summary>
		/// Compiles a <see cref="ITemplateSource"/> with a specified <see cref="ModelTypeInfo"/>
		/// </summary>
		/// <param name="templateSource">Template source</param>
		/// <param name="modelTypeInfo">Model type information</param>
		/// <returns>Compiled type in succeded. Compilation errors on fail</returns>
		public CompilationResult CompileSource(ITemplateSource templateSource, ModelTypeInfo modelTypeInfo)
		{
			if (templateSource == null)
			{
				throw new ArgumentNullException(nameof(templateSource));
			}

			string razorTemplate = GenerateRazorTemplate(templateSource, modelTypeInfo);
			var context = new CompilationContext(razorTemplate, configuration.Namespaces);

			CompilationResult compilationResult = compilerService.Compile(context);

			return compilationResult;
		}

		/// <summary>
		/// Compiles a page with a specified <param name="key" />
		/// </summary>
		/// <param name="key"></param>
		/// <returns>Compiled type in succeded. Compilation errors on fail</returns>
		public CompilationResult KeyCompile(string key)
		{
			ITemplateSource source = templateManager.Resolve(key);

			return CompileSource(source, null);
		}

		/// <summary>
		/// Activates a type using Activator from <see cref="IEngineConfiguration"/>
		/// </summary>
		/// <param name="compiledType"></param>
		/// <returns></returns>
		public TemplatePage Activate(Type compiledType)
		{
			return (TemplatePage)activator.CreateInstance(compiledType);
		}
	}
}
