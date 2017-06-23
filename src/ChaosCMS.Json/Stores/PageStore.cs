using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Json.Models;
using ChaosCMS.Stores;
using Microsoft.AspNetCore.Http;
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
        private readonly HttpContext httpContext;

        /// <summary>
        /// 
        /// </summary>
        protected internal override IList<TPage> Collection
        {
            get
            {
                var host = httpContext.Request.Host.Host;
                return base.Collection.Where(x => x.Hosts.Contains(host)).ToList();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public PageStore(IOptions<ChaosJsonStoreOptions> optionsAccessor, IHttpContextAccessor httpContextAccessor)
            : base(optionsAccessor)
        {
            this.httpContext = httpContextAccessor.HttpContext;
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

            var item = this.FilterCollection().FirstOrDefault(x => x.Url.Equals(urlPath, StringComparison.CurrentCultureIgnoreCase));
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

        /// <inhertdoc />
        public Task<string> GetPageTypeAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Type);
        }

        private List<TPage> FilterCollection()
        {
            var host = this.httpContext.Request.Host.Host;
            return this.Collection.Where(x => x.Hosts.Contains(host)).ToList();
        }
    }
}