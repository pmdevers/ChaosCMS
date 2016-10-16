using System;

namespace ChaosCMS.Razor.Templating
{
	public interface IPageLookup
	{
		IPageFactoryProvider PageFactoryProvider { get; }

		PageLookupResult GetPage(string key);
	}
}
