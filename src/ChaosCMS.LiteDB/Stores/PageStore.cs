using ChaosCMS.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ChaosCMS.LiteDB.Models;
using ChaosCMS.Models.Pages;

namespace ChaosCMS.LiteDB.Stores
{
    public class PageStore<TPage> : LiteDBStore<TPage>, IPageStore<TPage>, IPageContentStore<TPage>
        where TPage : LiteDBPage
    {
        public PageStore(ChaosLiteDBFactory factory) : base(factory)
        {
        }

        #region IPageStore<TPage>

        public Task<TPage> FindByStatusCodeAsync(int statusCode, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var page = this.Collection.FindOne(x => x.StatusCode == statusCode);
            return Task.FromResult(page);
        }

        public Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var page = this.Collection.FindOne(x => x.Url.Equals(urlPath));
            return Task.FromResult(page);
        }
        
        public Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Name);
        }

        public Task<string> GetPageTypeAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.PageType);
        }

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

        public Task<string> GetTemplateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Template);
        }

        public Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Url);
        }

        public Task SetNameAsync(TPage page, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.Name = name;
            return Task.FromResult(0);
        }

        public Task SetPageTypeAsync(TPage page, string pageType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            page.PageType = pageType;
            return Task.FromResult(0);
        }

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

        #endregion

        #region IPageContentStore<TPage>

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

        public Task<IList<string>> GetHostsAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Hosts);
        }

        public Task AddHostAsync(TPage page, string host, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.Hosts.Add(host);

            return Task.FromResult(0);
        }

        #endregion
    }
}