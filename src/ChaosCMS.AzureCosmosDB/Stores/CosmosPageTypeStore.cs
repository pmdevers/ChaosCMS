using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using ChaosCMS.AzureCosmosDB.Models;
using ChaosCMS.Stores;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.AzureCosmosDB.Stores
{
    public class CosmosPageTypeStore<TEntity> : CosmosDBStore<TEntity>, IPageTypeStore<TEntity>
        where TEntity : CosmosPageType
    {
        public CosmosPageTypeStore(IOptions<CosmosDBOptions> optionsAccessor) 
            : base(optionsAccessor)
        {
        }

        public Task<string> GetNameAsync(TEntity pageType, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (pageType == null)
            {
                throw new ArgumentNullException(nameof(pageType));
            }

            return Task.FromResult(pageType.Name);
        }

        public Task SetNameAsync(TEntity pageType, string name, CancellationToken cancellationToken = default(CancellationToken))
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
