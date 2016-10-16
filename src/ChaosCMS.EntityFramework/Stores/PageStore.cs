using ChaosCMS.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.EntityFramework
{
    /// <summary>
    /// Creates a new instance of a persistence store for pages.
    /// </summary>
    /// <typeparam name="TPage">The type of the class representing a page</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a page</typeparam>
    public class PageStore<TPage, TContext, TKey> : IPageStore<TPage>
        where TPage : class
        where TContext : DbContext
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}