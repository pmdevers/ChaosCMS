using System;

namespace ChaosCMS.Razor.Templating
{
	public interface IPageFactoryProvider
	{
		PageFactoryResult CreateFactory(string key);
	}
}
