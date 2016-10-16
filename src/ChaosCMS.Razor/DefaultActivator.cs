using System;

namespace ChaosCMS.Razor
{
	public class DefaultActivator : IActivator
	{
		public object CreateInstance(Type type)
		{
			return Activator.CreateInstance(type);
		}
	}
}
