using ChaosCMS.AzureCosmosDB.Models;
using ChaosCMS.Stores;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.AzureCosmosDB.Stores
{
    public class CosmosPageStore<TPage> : CosmosDBStore<TPage>, IPageStore<TPage>
        where TPage : CosmosPage
    {
        public CosmosPageStore(IOptions<CosmosDBOptions> optionsAccessor)
            :base(optionsAccessor)
        {
        }


        public Task AddHostAsync(TPage page, string host, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.Hosts.Add(host);

            return Task.FromResult(0);
        }

        public async Task<TPage> FindByStatusCodeAsync(int statusCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = await this.FindByPredicateAsync(x => x.StatusCode == statusCode, cancellationToken);
            return query.FirstOrDefault();
        }

        public async Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = await this.FindByPredicateAsync(x => x.Url == urlPath, cancellationToken);
            return query.FirstOrDefault();
        }

        public Task<IList<string>> GetHostsAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Hosts);
        }

        public Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Name);
        }

        public Task<string> GetPageTypeAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Type);
        }

        public Task<int> GetStatusCodeAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.StatusCode);
        }

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

        public Task<string> GetUrlAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Url);
        }

        public Task SetNameAsync(TPage page, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(0);
        }

        public Task SetPageTypeAsync(TPage page, string pageType, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(0);
        }

        public Task SetStatusCodeAsync(TPage page, int code, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(0);
        }

        public Task SetTemplateAsync(TPage page, string template, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(0);
        }

        public Task SetUrlAsync(TPage page, string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(0);
        }
    }
}
