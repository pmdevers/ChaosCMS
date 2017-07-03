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
    public class CarouselRenderer : IRenderer
        
    {
        /// <inheritdoc />
        public string TypeName => "carousel";

        /// <inheritdoc />
        public  Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
        {
            return Task.FromResult<IHtmlContent>(new HtmlString(content.Value));
        }

        /// <summary>
        ///
        /// </summary>
        public class CarouselOptions : RenderOptions
        {
        }

        /// <summary>
        ///
        /// </summary>
        public class SlideOptions : RenderOptions
        {
            /// <summary>
            ///
            /// </summary>
            public bool IsActive { get; set; }
        }
    }
}