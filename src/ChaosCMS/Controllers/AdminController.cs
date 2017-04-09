using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("admin", Name = "admin")]
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        public AdminController()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.View("admin.index.html");
        }
    }
}