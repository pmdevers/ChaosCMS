using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("error", Name ="Error")]
    public class ErrorController<TPage> : Controller
        where TPage : class
    {
        private readonly PageManager<TPage> pageManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        public ErrorController(PageManager<TPage> pageManager)
        {
            this.pageManager = pageManager;
        }


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
        public async Task<IActionResult> Get(int statuscode)
        {
            var page = await this.pageManager.FindByStatusCodeAsync(statuscode);

            if (page == null)
            {
                return View(statuscode.ToString());
            }
            var statusCode = this.pageManager.GetStatusCodeAsync(page);
            var template = await this.pageManager.GetTemplateAsync(page);
            return this.View(template, page);
        }

    }
}
