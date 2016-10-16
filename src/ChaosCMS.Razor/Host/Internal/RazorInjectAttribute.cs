using System;

namespace ChaosCMS.Razor.Host.Internal
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class RazorInjectAttribute : Attribute
	{
	}
}
