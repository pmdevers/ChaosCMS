using System.Threading.Tasks;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using ChaosCMS.Models.Pages;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    public class HtmlRenderer: IRenderer
    {

        /// <summary>
        ///
        /// </summary>
        public string TypeName
        {
            get
            {
                return "html";
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
        {
            return Task.FromResult<IHtmlContent>(new HtmlString(content.Value));
        }
    }
}