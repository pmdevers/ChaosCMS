using System;
using Microsoft.AspNetCore.Html;

namespace ChaosCMS.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaosHelper : ChaosHelper<dynamic>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ChaosHelper<TModel> : IChaosHelper<TModel>
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public IHtmlContent Raw(string test)
        {
            throw new ArgumentNullException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IHtmlContent Test()
        {
            return new HtmlString("test");
        }
    }
}
