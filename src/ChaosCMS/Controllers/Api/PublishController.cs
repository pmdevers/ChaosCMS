using ChaosCMS.Converters;
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
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    [Route("api/publish", Name = "publish")]
    public class PublishController<TSource, TDestination> : Controller
        where TSource : class, new()
    {
        private readonly IConverter<TSource, TDestination> converter;
        private readonly PageManager<TSource> pageManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="pageManager"></param>
        public PublishController(IConverter<TSource, TDestination> converter, PageManager<TSource> pageManager)
        {
            this.converter = converter;
            this.pageManager = pageManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var source = await this.pageManager.FindByIdAsync(id);
            var result = await this.converter.Convert(source);
            return Json(result);
        }
    }
}
