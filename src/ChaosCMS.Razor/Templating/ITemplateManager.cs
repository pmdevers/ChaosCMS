namespace ChaosCMS.Razor.Templating
{
	public interface ITemplateManager
	{
		ITemplateSource Resolve(string key);
	}
}
