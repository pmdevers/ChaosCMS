using System.Collections.Generic;
using ChaosCMS.Extensions;
using ChaosCMS.Hal;
using ChaosCMS.Models.Admin;
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
            var profile = new ProfileModel() { Name = "Admin" };
            var links = new List<Link>();
            return this.Hal(profile, links);
        }
    }
}