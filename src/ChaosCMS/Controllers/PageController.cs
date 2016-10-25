using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// A controller for handling a page
    /// </summary>
    [Route("api/page")]
    public class PageController<TPage> : Controller where TPage : class
    {
        private readonly PageManager<TPage> _pageManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        public PageController(PageManager<TPage> pageManager)
        {
            _pageManager = pageManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var page = this._pageManager.FindByIdAsync(id).Result;
            return this.Ok(page);
        }
    }
}
