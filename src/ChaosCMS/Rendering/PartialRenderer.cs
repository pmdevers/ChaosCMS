using System.Threading.Tasks;
using ChaosCMS.Extensions;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChaosCMS.Models.Pages;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    public class MacroRenderer : IRenderer
    {

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
        public Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
        {
            return Task.FromResult<IHtmlContent>(new HtmlString("test"));
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