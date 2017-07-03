using ChaosCMS.Converters;
using ChaosCMS.Managers;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosCMS.Controllers
{
    /// <summary>
    /// A controller for handling a page
    /// </summary>
    [Route("api/page", Name = "copy")]
    public class PageCopyController<TPage> : Controller
        where TPage : class
    {
        private readonly IConverter<TPage, TPage> converter;
        private readonly PageManager<TPage> manager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="manager"></param>
        public PageCopyController(IConverter<TPage, TPage> converter, PageManager<TPage> manager)
        {
            this.converter = converter;
            this.manager = manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("{id}/copy")]
        public async Task<IActionResult> Get(string id, CopyPageModel model)
        {
            var source = await this.manager.FindByIdAsync(id);
            var result = await this.converter.Convert(source);

            if (result.Succeeded)
            {
                await this.manager.SetUrlAsync(result.Destination, model.NewUrl);
                await this.manager.UpdateAsync(result.Destination);
            }

            return Json(result);
        }

    }
}
