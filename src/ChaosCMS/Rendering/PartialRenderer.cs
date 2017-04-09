using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class MacroRenderer<TContent> : IRenderer<TContent>
        where TContent : class
    {
        private readonly ContentManager<TContent> manager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="manager"></param>
        public MacroRenderer(ContentManager<TContent> manager)
        {
            this.manager = manager;
        }

        /// <summary>
        ///
        /// </summary>
        public string TypeName => "macro";

        /// <summary>
        ///
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(IChaos<TContent> chaos, TContent content)
        {
            var options = await this.manager.GetValueAsync<MacroOptions, TContent>(content);
            return await chaos.Helper.PartialAsync(options.ViewName, content);
        }

        /// <summary>
        ///
        /// </summary>
        public class MacroOptions : RenderOptions
        {
            /// <summary>
            ///
            /// </summary>
            public string ViewName { get; set; }
        }
    }
}