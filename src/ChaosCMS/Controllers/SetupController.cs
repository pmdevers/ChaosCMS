using ChaosCMS.Managers;
using ChaosCMS.Models.Setup;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("admin/setup", Name = "Setup")]
    public class SetupController<TPage> : Controller
        where TPage : class
    {
        private readonly PageManager<TPage> pageManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        public SetupController(PageManager<TPage> pageManager)
        {
            this.pageManager = pageManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View("wizzard");
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(SetupModel model)
        {
            return Ok();
        }
    }
}
