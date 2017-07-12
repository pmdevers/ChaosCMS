using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChaosCMS.Json.Models;
using ChaosCMS.Models.Pages;
using ChaosCMS.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ChaosCMS.Json.Stores
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    public class PageStore<TPage> : JsonStore<TPage>, IPageStore<TPage>, IPageContentStore<TPage>
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
        public Task<TPage> FindByRequestAsync(HttpRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FirstOrDefault(x => x.Host == request.Host.Host && x.Url == request.Path.Value);
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
        public Task<ChaosPaged<TPage>> FindPagedAsync(HttpRequest request, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var items = this.Collection.Where(x => x.Host == request.Host.Host).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return Task.FromResult(new ChaosPaged<TPage>
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = this.Collection.Count(),
                Items = items
            });
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
        public virtual Task SetNameAsync(TPage page, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.Name = name;

            return Task.FromResult(0);
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

        /// <inhertdoc />
        public Task SetUrlAsync(TPage page, string url, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.Url = url;
            return Task.FromResult(0);
        }

        /// <inhertdoc />
        public Task SetTemplateAsync(TPage page, string template, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.Template = template;
            return Task.FromResult(0);
        }

        /// <inhertdoc />
        public Task SetStatusCodeAsync(TPage page, int code, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.StatusCode = code;
            return Task.FromResult(0);
        }

        /// <inhertdoc />
        public Task SetPageTypeAsync(TPage page, string pageType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.Type = pageType;
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task<IList<Content>> GetContentAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Content);
        }

        /// <inheritdoc />
        public Task SetContentAsync(TPage page, IEnumerable<Content> content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            foreach (var item in content)
            {
                page.Content.Add(item);
            }

            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task SetOriginAsync(TPage page, string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.Origin = id;
            return Task.FromResult(0);
        }

        /// <Inhertdoc />
        public Task<string> GetHostAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Host);
        }
        /// <inheritdoc />
        public Task SetHostAsync(TPage page, string host, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.Host = host;
            return Task.FromResult(0);
        }
    }
}