using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Routing;

namespace ChaosCMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api")]
    public class RootController : Controller
    {
        private readonly ApplicationPartManager manager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="partManager"></param>
        public RootController(ApplicationPartManager partManager)
        {
            manager = partManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var parts = manager.ApplicationParts.FirstOrDefault(x => x is ChaosTypesPart) as ChaosTypesPart;
            var links = new List<Link>();
            if (parts != null)
            {
                foreach (var typeInfo in parts.Types)
                {
                    var attribute = (RouteAttribute)typeInfo.GetCustomAttribute(typeof(RouteAttribute));
                    links.Add(new Link(attribute.Name, attribute.Template));
                }
            }

            return this.Hal(links);
        }
    }
}