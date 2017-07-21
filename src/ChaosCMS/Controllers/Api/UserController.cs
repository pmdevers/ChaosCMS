using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    [Route("api/user", Name = "User")]
    [Authorize]
    public class UserController<TUser> : Controller
        where TUser : class
    {
        private readonly UserManager<TUser> userManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(UserManager<TUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet( Name = "currentuser")]
        public IActionResult Get()
        {
            return Ok(this.User);
        }
    }
}