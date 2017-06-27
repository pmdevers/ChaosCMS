using ChaosCMS.LiteDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using ChaosCMS.Stores;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.LiteDB.Stores
{
    public class PageTypeStore<TPageType> : LiteDBStore<TPageType>, IPageTypeStore<TPageType>
        where TPageType : LiteDBPageType
    {
        public PageTypeStore(IOptions<ChaosLiteDBStoreOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

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
