using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.Stores
{
    /// <summary>
    /// Provides an abstraction for a storage and management of content.
    /// </summary>
    /// <typeparam name="TContent">The type that represents a content.</typeparam>
    public interface IContentStore<TContent> : IDisposable
        where TContent : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        Task<TContent> FindByIdAsync(string contentId, CancellationToken cancelationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> CreateAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> UpdateAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosResult> DeleteAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ChaosPaged<TContent>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TContent> FindByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetIdAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetNameAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetTypeAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetValueAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TContent>> GetChildrenAsync(TContent content, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddChildAsync(TContent parent, TContent child, CancellationToken cancellationToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TContent> FindByPageIdAsync(string pageId, string name, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TContent> FindChildByNameAsync(TContent parent, string name, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetValueAsync(TContent content, string value, CancellationToken cancellationToken);
    }
}
