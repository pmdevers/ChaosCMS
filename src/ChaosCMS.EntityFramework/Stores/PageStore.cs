using ChaosCMS.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Entityframework;

namespace ChaosCMS.EntityFramework
{
    /// <summary>
    /// Creates a new instance of a persistence store for pages.
    /// </summary>
    /// <typeparam name="TPage">The type of the class representing a page</typeparam>
    /// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a page</typeparam>
    public class PageStore<TPage, TContext, TKey> : IPageStore<TPage>
        where TPage : ChaosPage<TKey>
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

        /// <inheritdoc />
        public Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ChaosResult> UpdateAsync(TPage page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<TPage> FindByIdAsync(string pageId, CancellationToken cancelationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetIdAsync(TPage page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc />
        public Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetTemplateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Template);
        }

        
    }
}