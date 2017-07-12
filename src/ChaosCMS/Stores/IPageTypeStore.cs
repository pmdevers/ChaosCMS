using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.Stores
{
    /// <summary>
    /// A interface that describes a PageType store.
    /// </summary>
    /// <typeparam name="TPageType">The type representing a pagetype</typeparam>
    public interface IPageTypeStore<TPageType> : IDisposable
        where TPageType : class
    {

        #region CRUD
        /// <summary>
        /// This stores a new instance/>
        /// </summary>
        /// <param name="pageType">The PageType</param>
        /// <param name="cancellationToken">The CancellationToken </param>
        /// <returns></returns>
        Task<ChaosResult> CreateAsync(TPageType pageType, CancellationToken cancellationToken);

        #region READ

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosPaged<TPageType>> FindPagedAsync(HttpRequest request, int page, int itemsPerPage, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TPageType> FindByIdAsync(string id, CancellationToken cancellationToken);

        #endregion

        /// <summary>
        /// This updates a pagetype/>
        /// </summary>
        /// <param name="pageType">The PageType</param>
        /// <param name="cancellationToken">The CancellationToken </param>
        /// <returns></returns>
        Task<ChaosResult> UpdateAsync(TPageType pageType, CancellationToken cancellationToken);

        /// <summary>
        /// This deletes the instance/>
        /// </summary>
        /// <param name="pageType">The PageType</param>
        /// <param name="cancellationToken">The CancellationToken </param>
        /// <returns></returns>
        Task<ChaosResult> DeleteAsync(TPageType pageType, CancellationToken cancellationToken);
        #endregion

        #region Gets and Sets

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetIdAsync(TPageType pageType, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the name of the PageType
        /// </summary>
        /// <param name="pageType">The pageType to get the namefrom</param>
        /// <param name="cancellationToken">The CancellationToken</param>
        /// <returns></returns>
        Task<string> GetNameAsync(TPageType pageType, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageType"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetNameAsync(TPageType pageType, string name, CancellationToken cancellationToken);

        #endregion
    }
}
