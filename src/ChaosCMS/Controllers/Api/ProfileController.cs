using System.Collections.Generic;
using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers.Api
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/profile/", Name = "Profile")]
    public class ProfileController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        public ProfileController()
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
            var profile = this.User;
            var links = new List<Link>();
            return this.Hal(profile, links);
        }
    }
}