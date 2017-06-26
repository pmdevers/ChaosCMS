using ChaosCMS.Extensions;
using ChaosCMS.Managers;
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
    /// <typeparam name="TPageType"></typeparam>
    [Route("api/pagetype", Name = "pagetype")]
    public class PageTypeController<TPageType> : Controller where TPageType : class
    {
        private readonly PageTypeManager<TPageType> manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageTypeManager"></param>
        public PageTypeController(PageTypeManager<TPageType> pageTypeManager)
        {
            this.manager = pageTypeManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(int page = 1, int itemsPerPage = 25)
        {
            var pages = this.manager.FindPagedAsync(page, itemsPerPage).Result;
            return this.PagedHal(pages, item => this.CreateEmbeddedResponse(this.manager, item), "pagetypes");
        }
    }
}
