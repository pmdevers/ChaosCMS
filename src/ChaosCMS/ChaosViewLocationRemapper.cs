using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public class ChaosViewLocationRemapper : IViewLocationExpander
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var locations = new List<string> { "/Templates/{0}.cshtml", "/Macros/{0}.cshtml", "ChaosCMS.{0}" };
            //locations.AddRange(viewLocations);

            return locations.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}