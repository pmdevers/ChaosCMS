using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChaosCMS.Managers;
using ChaosCMS.Razor;
using ChaosCMS.Razor.Host.Internal;
using ChaosCMS.Rendering;
using Microsoft.AspNetCore.Http;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TPage"></typeparam>
    public class ChaosRazorPage<TModel, TPage> : TemplatePage<TModel>
    {
        /// <summary>
        /// 
        /// </summary>
        [RazorInject]
        public IHttpContextAccessor ContextAccessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [RazorInject]
        public IChaosHelper<TModel, TPage> Chaos { get; set; }
        
        #region Overrides of TemplatePage

        /// <inheritdoc />
        public override Task ExecuteAsync()
        {
            return Task.FromResult(0);
        }

        #endregion
    }
}
