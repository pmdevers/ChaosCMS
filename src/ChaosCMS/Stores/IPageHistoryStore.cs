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
    public interface IPageHistoryStore<TPage>
        where TPage : class
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DateTime> GetCreationDateAsync(TPage page, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="creationDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetCreationDateAsync(TPage page, DateTime creationDate, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DateTime?> GetModifiedDateAsync(TPage page, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="modifiedDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetModifiedDateAsync(TPage page, DateTime modifiedDate, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetCreatedByAsync(TPage page, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetCreatedByAsync(TPage page, string username, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetModifiedByAsync(TPage page, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="username"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetModifiedByAsync(TPage page, string username, CancellationToken cancellationToken);
    }
}
