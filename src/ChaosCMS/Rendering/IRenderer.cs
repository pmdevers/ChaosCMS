using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public interface IRenderer<TContent>
        where TContent : class
    {
        /// <summary>
        ///
        /// </summary>
        string TypeName { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chaos"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderAsync(IChaos<TContent> chaos, TContent content);
    }
}