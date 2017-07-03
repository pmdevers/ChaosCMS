using System.Threading.Tasks;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;
using ChaosCMS.Models.Pages;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    public class LinkContentRenderer : IRenderer
    {

        /// <summary>
        ///
        /// </summary>
        public string TypeName => "ContentLink";

        /// <summary>
        ///
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
        {
            //var value = await this.contentManager.GetValueAsync(content);
            //var linkedContent = await this.contentManager.FindByIdAsync(value);
            //return await chaos.RenderAsync(linkedContent);

            return Task.FromResult(chaos.Helper.Raw("test"));
        }
    }
}