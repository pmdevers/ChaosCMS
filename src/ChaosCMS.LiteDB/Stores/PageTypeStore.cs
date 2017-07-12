using ChaosCMS.LiteDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using ChaosCMS.Stores;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChaosCMS.LiteDB.Stores
{
    public class PageTypeStore<TPageType> : LiteDBStore<TPageType>, IPageTypeStore<TPageType>
        where TPageType : LiteDBPageType
    {
        public PageTypeStore(ChaosLiteDBFactory factory) : base(factory)
        {
        }

        /// <inheritdoc />
        public override Task<ChaosPaged<TPageType>> FindPagedAsync(HttpRequest request, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var count = this.Collection.Count(x=>x.Host == request.Host.Host);
            var pages = this.Collection.Find(x => x.Host == request.Host.Host, ((page - 1) * itemsPerPage), itemsPerPage);

            var paged = new ChaosPaged<TPageType>()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                Items = pages,
                TotalItems = count
            };

            return Task.FromResult(paged);
        }

        /// <inheritdoc />
        public Task<string> GetNameAsync(TPageType pageType, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            return Task.FromResult(pageType.Name);
        }

        /// <inheritdoc />
        public Task SetNameAsync(TPageType pageType, string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }
            pageType.Name = name;
            return Task.FromResult(0);
        }
    }
}
