using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    [Route("/api/page", Name ="Content")]
    public class PageContentController<TPage> : Controller
        where TPage : class
    {
        private readonly PageManager<TPage> manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        public PageContentController(PageManager<TPage> pageManager)
        {
            this.manager = pageManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/content", Name = "page-content")]
        public async Task<IActionResult> Get(string id)
        {
            var page = await this.manager.FindByIdAsync(id);
            if (page == null)
            {
                return this.ChaosResults(this.manager.ErrorDescriber.PageIdNotFound(id));
            }

            var content = await this.manager.GetContentAsync(page);

            return this.Ok(content);
        }
    }
}
