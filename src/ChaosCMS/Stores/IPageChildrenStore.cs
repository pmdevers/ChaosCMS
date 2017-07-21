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
    public interface IPageChildrenStore<TPage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<TPage>> GetChildrenAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="child"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddChildAsync(TPage page, TPage child, CancellationToken cancellationToken);
    }
}
