using ChaosCMS.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public interface IPageContentStore<TPage>
        where TPage : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Content>> GetContentAsync(TPage page, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetContentAsync(TPage page, List<Content> content, CancellationToken cancellationToken);
    }
}
