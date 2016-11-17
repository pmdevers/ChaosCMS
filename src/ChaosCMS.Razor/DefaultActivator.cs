using System;
using System.Reflection;

using ChaosCMS.Razor.Host.Internal;

namespace ChaosCMS.Razor
{
	public class DefaultActivator : IActivator
	{
        IServiceProvider services;
        public DefaultActivator(IServiceProvider services)
        {
            this.services = services;
        }

		public object CreateInstance(Type type)
		{
			var obj = Activator.CreateInstance(type);
            foreach(var prop in obj.GetType().GetProperties())
            {
                var attribute = prop.GetCustomAttribute<RazorInjectAttribute>();
                if (attribute != null)
                {
                    var value = services.GetService(prop.PropertyType);
                    prop.SetValue(obj, value);
                }
            }
            
            return obj;
		}

	    public void ActivatePage(TemplatePage page)
	    {
            foreach (var prop in page.GetType().GetProperties())
            {
                var attribute = prop.GetCustomAttribute<RazorInjectAttribute>();
                if (attribute != null)
                {
                    var value = services.GetService(prop.PropertyType);
                    prop.SetValue(page, value);
                }
            }
        }
    }
}
