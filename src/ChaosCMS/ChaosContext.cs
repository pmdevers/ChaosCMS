using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ChaosCMS.Managers;

using Microsoft.AspNetCore.Http;

namespace ChaosCMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public class ChaosContext<TPage> : IChaosContext<TPage>
        where TPage : class
    {
        private readonly PageManager<TPage> pageManager;

        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageManager"></param>
        /// <param name="contextAccessor"></param>
        public ChaosContext(PageManager<TPage> pageManager, IHttpContextAccessor contextAccessor)
        {
            this.pageManager = pageManager;
            this.contextAccessor = contextAccessor;
        }

        private TPage currentPage;
        #region Implementation of IChaosContext<TPage,TContent>

        /// <inheritdoc />
        public TPage CurrentPage
        {
            get
            {
                if (this.currentPage == null)
                {
                    var context = this.contextAccessor.HttpContext;
                    this.currentPage = this.pageManager.FindByUrlAsync(context.Request.Path.Value).Result;
                }
                return this.currentPage;
            }
        }

        #endregion
    }
}
