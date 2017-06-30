using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using ChaosCMS.Models.Pages;

namespace ChaosCMS.Rendering
{
    /// <summary>
    ///
    /// </summary>
    public interface IRenderer
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
        Task<IHtmlContent> RenderAsync(IChaos chaos, Content content);
    }
}