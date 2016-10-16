using System;
using System.Collections.Generic;
using ChaosCMS.Razor.Compilation;
using ChaosCMS.Razor.Internal;

namespace ChaosCMS.Razor
{
	public class EngineConfiguration : IEngineConfiguration
	{
        /// <summary>
        /// Activator used to create an instance of the compiled templates
        /// </summary>
        public IActivator Activator { get; set; } = new DefaultActivator();

        /// <summary>
        /// Class used to compile razor templates into *.cs file
        /// </summary>
        public IRazorTemplateCompiler RazorTemplateCompiler { get; set; } = new DefaultRazorTemplateCompiler();

        /// <summary>
        /// Class used to compile razor templates
        /// </summary>
        public ICompilerService CompilerService { get; set; } = new RoslynCompilerService(new UseEntryAssemblyMetadataResolver());

		/// <summary>
		/// Additional namespace to include into template (_ViewImports like)
		/// </summary>
		public ISet<string> Namespaces { get;  set; } = new HashSet<string>();

		/// <summary>
        /// 
        /// </summary>
        public PreRenderActionList PreRenderCallbacks { get;  set; } = new PreRenderActionList();

    }
}
