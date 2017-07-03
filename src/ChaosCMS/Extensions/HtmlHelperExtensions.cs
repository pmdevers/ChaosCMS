using ChaosCMS;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    ///
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IChaos Chaos<TModel>(this IHtmlHelper<TModel> helper)
        {
            var service = (IChaos)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IChaos));
            service.Helper = helper;
            return service;
        }
    }
}