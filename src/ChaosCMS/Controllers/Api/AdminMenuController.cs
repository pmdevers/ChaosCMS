using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosCMS.Hal;
using ChaosCMS.Extensions;
using ChaosCMS.Models.Admin;

namespace ChaosCMS.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/admin/", Name = "AdminRoot")]
    public class AdminApiController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public AdminApiController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            var menu = new List<Link>() {
                new Link("profile", "http://localhost:17706/api/profile"),
                new Link("menu", "/api/menus"),
                new Link("pages", "/api/pages"),
                new Link("users", "/api/users")
            };
            return this.Hal(menu);
        }
    }
}
