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
    /// <typeparam name="TContent"></typeparam>
    public interface IPageContentStore<TPage, TContent>
        where TPage : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TContent>> GetPageContentAsync(TPage page, CancellationToken cancellationToken);
    }
}
