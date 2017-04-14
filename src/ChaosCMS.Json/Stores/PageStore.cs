using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Json.Models;
using ChaosCMS.Stores;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Json.Stores
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public class PageStore<TPage> : JsonStore<TPage>, IPageStore<TPage>
        where TPage : JsonPage, new()
    {
        /// <summary>
        ///
        /// </summary>
        public PageStore(IOptions<ChaosJsonStoreOptions> optionsAccessor)
            : base(optionsAccessor)
        {
        }

        /// <inheritdoc />
        public Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var items = this.Collection.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return Task.FromResult(new ChaosPaged<TPage>
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = this.Collection.Count(),
                Items = items
            });
        }

        /// <inheritdoc />
        public virtual Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FirstOrDefault(x => x.Url.Equals(urlPath, StringComparison.CurrentCultureIgnoreCase));
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<TPage> FindByStatusCodeAsync(int statusCode, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FirstOrDefault(x => x.StatusCode == statusCode);
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public virtual Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Name);
        }

        /// <inheritdoc />
        public virtual Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Url);
        }

        /// <inheritdoc />
        public Task<string> GetTemplateAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Template);
        }

        /// <inheritdoc />
        public Task<int> GetStatusCodeAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.StatusCode);
        }
    }
}