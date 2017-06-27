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
    public class ErrorController<TPage, TContent> : Controller
        where TPage : class
        where TContent : class
    {
        private readonly PageManager<TPage, TContent> pageManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        public ErrorController(PageManager<TPage, TContent> pageManager)
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
                return View(statuscode);
            }
            var statusCode = this.pageManager.GetStatusCodeAsync(page);
            var template = await this.pageManager.GetTemplateAsync(page);
            return this.View(template, page);
        }

    }
}
