using System.Threading.Tasks;
using ChaosCMS.Managers;
using Microsoft.AspNetCore.Html;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class StringRenderer<TContent> : IRenderer<TContent>
        where TContent : class
    {
        private readonly ContentManager<TContent> contentManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="contentManager"></param>
        public StringRenderer(ContentManager<TContent> contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        ///
        /// </summary>
        public string TypeName => "string";

        /// <summary>
        ///
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<IHtmlContent> RenderAsync(IChaos<TContent> chaos, TContent content)
        {
            var value = await this.contentManager.GetValueAsync(content);
            return new HtmlString(value);
        }
    }
}