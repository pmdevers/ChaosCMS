using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ChaosCMS.Models.Pages;

namespace ChaosCMS
{
    /// <summary>
    ///
    /// </summary>
    public interface IChaos
    {
        /// <summary>
        ///
        /// </summary>
        string Name { get; }

        /// <summary>
        ///
        /// </summary>
        IHtmlHelper Helper { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderAsync(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        JObject GetJson();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<IHtmlContent> Scripts();

        /// <summary>
        ///
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task AddScript(IHtmlContent content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderAsync(Content content);
    }
}