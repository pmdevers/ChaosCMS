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
    public class DefaultRenderer : IRenderer
    {

        /// <summary>
        ///
        /// </summary>
        public string TypeName => "default";

        /// <summary>
        ///
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(IChaos chaos, Content content)
        {
            var tagBuilder = new TagBuilder(content.Type);
            var value = new RenderOptions();

            value.CssClass.Reverse();
            value.CssClass.ForEach(tagBuilder.AddCssClass);
            tagBuilder.MergeAttributes(value.Attributes);

            var builder = tagBuilder.InnerHtml;
            foreach (var child in content.Children)
            {
                var html = await chaos.RenderAsync(child);
                builder = builder.AppendHtml(html);
            }

            return tagBuilder;
        }
    }
}