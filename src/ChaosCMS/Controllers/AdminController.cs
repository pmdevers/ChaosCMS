using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    [Route("admin", Name = "admin")]
    public class AdminController<TUser> : Controller
        where TUser : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        public AdminController(UserManager<TUser> userManager)
        {
              
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.View("admin");
        }
    }
}
