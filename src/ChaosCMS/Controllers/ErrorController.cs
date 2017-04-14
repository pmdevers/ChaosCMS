using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("error", Name ="Error")]
    public class ErrorController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            return View("GenericError");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statuscode"></param>
        /// <returns></returns>
        [Route("{statuscode}")]
        [HttpGet]
        public IActionResult Get(int statuscode)
        {

            return View(statuscode);
        }

    }
}
