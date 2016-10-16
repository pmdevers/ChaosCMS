using ChaosCMS.Razor.Host;
using ChaosCMS.Razor.Templating;

namespace ChaosCMS.Razor.Compilation
{
	public interface IRazorTemplateCompiler
	{
		string CompileTemplate(ChaosRazorHost host, ITemplateSource templateSource);
	}
}
