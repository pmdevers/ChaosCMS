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
    }
}