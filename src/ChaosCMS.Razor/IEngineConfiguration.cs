using System.Collections.Generic;
using ChaosCMS.Razor.Compilation;
using ChaosCMS.Razor.Internal;

namespace ChaosCMS.Razor
{
	public interface IEngineConfiguration
	{
		/// <summary>
		/// Activator used to create an instance of the compiled templates
		/// </summary>
		IActivator Activator { get; set; }

		/// <summary>
		/// Class used to compile razor templates into *.cs file
		/// </summary>
		IRazorTemplateCompiler RazorTemplateCompiler { get; set; }

		/// <summary>
		/// Class used to compile razor templates
		/// </summary>
		ICompilerService CompilerService { get; set; }

		/// <summary>
		/// Additional namespace to include into template (_ViewImports like)
		/// </summary>
		ISet<string> Namespaces { get; set; }

        /// <summary>
        /// 
        /// </summary>
		PreRenderActionList PreRenderCallbacks { get; set; }
	}
}
