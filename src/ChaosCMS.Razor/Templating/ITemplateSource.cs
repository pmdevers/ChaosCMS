using System.IO;

namespace ChaosCMS.Razor.Templating
{
	public interface ITemplateSource
	{
		string Content { get; }

		string FilePath { get; }

		string TemplateKey { get; }

		TextReader CreateReader();
	}
}
