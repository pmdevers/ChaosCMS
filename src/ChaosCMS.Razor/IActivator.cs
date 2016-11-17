using System;

namespace ChaosCMS.Razor
{
	public interface IActivator
	{
		object CreateInstance(Type type);

	    void ActivatePage(TemplatePage page);
	}
}
