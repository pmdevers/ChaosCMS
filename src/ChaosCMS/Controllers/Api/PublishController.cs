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
        where TDestination : class, new()
    {
        private readonly IConverter<TSource, TDestination> converter;
        private readonly PageManager<TSource> sourceManager;
        private readonly PageManager<TDestination> destinationManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="sourceManager"></param>
        /// <param name="destinationManager"></param>
        public PublishController(IConverter<TSource, TDestination> converter, PageManager<TSource> sourceManager, PageManager<TDestination> destinationManager)
        {
            this.converter = converter;
            this.sourceManager = sourceManager;
            this.destinationManager = destinationManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/publish" , Name = "publish-page")]
        public async Task<IActionResult> Publish(string id)
        {
            var source = await this.sourceManager.FindByIdAsync(id);
            if(source == null)
            {
                return this.ChaosResults(this.sourceManager.ErrorDescriber.PageIdNotFound(id));
            }
            var result = await this.converter.Convert(source);
            return this.ChaosResults(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/unpublish", Name = "unpublish-page")]
        public async Task<IActionResult> Unpublish(string id)
        {
            var origin = await this.destinationManager.FindByOriginAsync(id);
            if (origin == null)
            {
                return this.ChaosResults(this.sourceManager.ErrorDescriber.PageIdNotFound(id));
            }

            var result = await this.destinationManager.DeleteAsync(origin);
            return this.ChaosResults(result);
        }
    }
}
