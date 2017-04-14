using System;
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
        /// <param name="urlPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken);

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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken);

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
        /// <returns></returns>
        Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> CreateAsync(TPage page, CancellationToken cancellationToken);

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
        Task<int> GetStatusCodeAsync(TPage page, CancellationToken cancellationToken);
    }
}