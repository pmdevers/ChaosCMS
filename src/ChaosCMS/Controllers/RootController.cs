using System;
using System.Linq;
using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    public class RootController : Controller
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public RootController(IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {

            return this.Hal(new[]
            {
                new Link("pages", "/api/pages")
            });
        }
    }
}
