using ChaosCMS.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ChaosCMS.LiteDB.Models;
using ChaosCMS.Models.Pages;
using Microsoft.AspNetCore.Http;
using System.Linq;
using LiteDB;

namespace ChaosCMS.LiteDB.Stores
{
    public class PageStore<TPage> : LiteDBStore<TPage>, IPageStore<TPage>, IPageContentStore<TPage>, IPageHistoryStore<TPage>, IPageChildrenStore<TPage>
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

        public override Task<ChaosPaged<TPage>> FindPagedAsync(HttpRequest request, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var total = this.Collection.Count(x => x.Host == request.Host.Host);
            var results = this.Collection.Find(x => x.Host == request.Host.Host, (page - 1) * itemsPerPage, itemsPerPage);

            return Task.FromResult(new ChaosPaged<TPage>
            {
                TotalItems = total,
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                Items = results
            });
        }

        public Task<TPage> FindByRootAsync(HttpRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var page = this.Collection.FindOne(x => x.Url.Equals("/") && x.Host == request.Host.Host);
            return Task.FromResult(page);
        }

        public Task<TPage> FindByRequestAsync(HttpRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var page = this.Collection.FindOne(x => x.Url.Equals(request.Path.Value) && x.Host == request.Host.Host);
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

        public Task<DateTime> GetCreationDateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.Id.CreationTime);
        }

        public Task SetCreationDateAsync(TPage page, DateTime creationDate, CancellationToken cancellationToken)
        {
            // Do nothing creation date is part of the objectId

            return Task.FromResult(0);
        }

        public Task<DateTime?> GetModifiedDateAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.ModifiedDate);
        }

        public Task SetModifiedDateAsync(TPage page, DateTime modifiedDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.ModifiedDate = modifiedDate;

            return Task.FromResult(0);
        }

        public Task<string> GetCreatedByAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.CreatedBy);
        }

        public Task SetCreatedByAsync(TPage page, string username, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.CreatedBy = username;

            return Task.FromResult(0);
        }

        public Task<string> GetModifiedByAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return Task.FromResult(page.ModifiedBy);
        }

        public Task SetModifiedByAsync(TPage page, string username, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.ModifiedBy = username;

            return Task.FromResult(0);
        }

        public Task<IList<TPage>> GetChildrenAsync(TPage page, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            var ids = page.Children.Select(x => new BsonValue(new ObjectId(x.Value)));

            var result = this.Collection.Find(Query.In("_id", ids)).ToList();

            return Task.FromResult<IList<TPage>>(result);

        }

        public Task AddChildAsync(TPage page, TPage child, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.Children[child.Name] = ConvertIdToString(child.Id);
            
            return Task.FromResult(0);
        }


        #endregion
    }
}