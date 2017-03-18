using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    [Route("admin", Name = "admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {
              
        }

        [HttpGet]
        public IActionResult Get()
        {
            return this.View("admin");
        }
    }
}
