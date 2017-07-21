using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Routing;
using ChaosCMS.Models.Root;
using ChaosCMS.Managers;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api")]
    public class RootController<TPage> : Controller
        where TPage : class
    {
        private readonly PageManager<TPage> pageManager;
        private readonly ApplicationPartManager partManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageManager"></param>
        /// <param name="partManager"></param>
        public RootController(PageManager<TPage> pageManager, ApplicationPartManager partManager)
        {
            this.pageManager = pageManager;
            this.partManager = partManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var parts = partManager.ApplicationParts.FirstOrDefault(x => x is ChaosTypesPart) as ChaosTypesPart;
            var links = new List<Link>();
            //if (parts != null)
            //{
            //    foreach (var typeInfo in parts.Types)
            //    {
            //        var attribute = (RouteAttribute)typeInfo.GetCustomAttribute(typeof(RouteAttribute));
            //        if (attribute.Template.StartsWith("api"))
            //            links.Add(new Link(attribute.Name, attribute.Template));
            //    }
            //}
            var homepage = await this.pageManager.FindRootAsync();
            var id = await this.pageManager.GetIdAsync(homepage);

            links.Add(new Link("Homepage", Url.RouteUrl("page", new { id = id })));
            links.Add(new Link("User", Url.RouteUrl("currentuser")));
            links.Add(new Link("Pages", Url.RouteUrl("pages")));


            //var model = new ApiRoot
            //{
            //    CurrentUserUrl = "/api/user",
            //    GetHomepageUrl = this.SelfLink(pageManager, homepage).Href
            //};



            return this.Hal(links);
        }
    }
}