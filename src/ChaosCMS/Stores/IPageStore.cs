using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.Stores
{
    /// <summary>
    /// Provides an abstraction for a storage and management of pages.
    /// </summary>
    /// <typeparam name="TPage">The type that represents a page.</typeparam>
    public interface IPageStore<TPage> : IDisposable
        where TPage : class
    {
        #region CRUD Operations
        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> CreateAsync(TPage page, CancellationToken cancellationToken);

        #region READ
        /// <summary>
        ///
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        Task<TPage> FindByIdAsync(string pageId, CancellationToken cancelationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TPage> FindByOriginAsync(string origin, CancellationToken cancellationToken);

        /// <summary>
        ///
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken);

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> UpdateAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> DeleteAsync(TPage page, CancellationToken cancellationToken);

        #endregion

        #region Gets and Sets

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TPage> FindByStatusCodeAsync(int statusCode, CancellationToken cancellationToken);


        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetIdAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetNameAsync(TPage page, string name, CancellationToken cancellationToken);


        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetUrlAsync(TPage page, string url, CancellationToken cancellationToken);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetTemplateAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="template"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetTemplateAsync(TPage page, string template, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetStatusCodeAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetStatusCodeAsync(TPage page, int code, CancellationToken cancellationToken);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetPageTypeAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetPageTypeAsync(TPage page, string pageType, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetOriginAsync(TPage page, string id, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<string>> GetHostsAsync(TPage page, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="host"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddHostAsync(TPage page, string host, CancellationToken cancellationToken);

        #endregion
    }
}