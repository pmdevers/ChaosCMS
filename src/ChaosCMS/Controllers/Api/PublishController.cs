using ChaosCMS.Converters;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChaosCMS.Extensions;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    [Route("api/page", Name = "publish")]
    public class PublishController<TSource, TDestination> : Controller
        where TSource : class, new()
    {
        private readonly IConverter<TSource, TDestination> converter;
        private readonly PageManager<TSource> pageManager;
        private readonly ChaosErrorDescriber errorDescriber;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="pageManager"></param>
        /// <param name="errorDescriber"></param>
        public PublishController(IConverter<TSource, TDestination> converter, PageManager<TSource> pageManager, ChaosErrorDescriber errorDescriber)
        {
            this.converter = converter;
            this.pageManager = pageManager;
            this.errorDescriber = errorDescriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/publish")]
        public async Task<IActionResult> Get(string id)
        {
            var source = await this.pageManager.FindByIdAsync(id);
            if(source == null)
            {
                return this.ChaosResults(errorDescriber.PageIdNotFound(id));
            }
            var result = await this.converter.Convert(source);
            return this.ChaosResults(result);
        }
    }
}
