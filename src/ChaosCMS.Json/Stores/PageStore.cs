using ChaosCMS.Json.Models;
using ChaosCMS.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            :base(optionsAccessor)
        {
            
        }
        /// <inheritdoc />
        public Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var allItems = ReadFile();
            var items = allItems.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            return Task.FromResult(new ChaosPaged<TPage>
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = allItems.Count(),
                Items = items
            });

        }

        /// <inheritdoc />
        public virtual Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var item = ReadFile().FirstOrDefault(x => x.Url.Equals(urlPath, StringComparison.CurrentCultureIgnoreCase));
            return Task.FromResult(item);
        }
        
        /// <inheritdoc />
        public virtual Task<string> GetNameAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
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
            ThrowIfDisposed();
            if(page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Url);
        }

        /// <inheritdoc />
        public Task<string> GetTemplateAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(page.Template);
        }
    }
}