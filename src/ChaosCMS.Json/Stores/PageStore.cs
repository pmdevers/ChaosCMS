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
    public class PageStore<TPage> : IPageStore<TPage>
        where TPage : JsonPage, new()
    {
        private bool isDisposed = false;
        private static object lockObject = new object();

        /// <summary>
        /// 
        /// </summary>
        public PageStore(IOptions<ChaosJsonStoreOptions> optionsAccessor)
        {
            this.Options = optionsAccessor?.Value ?? new ChaosJsonStoreOptions();
        }

        /// <summary>
        /// The <see cref="ChaosJsonStoreOptions"/> used to configure Chaos Json Store.
        /// </summary>
        protected internal ChaosJsonStoreOptions Options { get; }

        /// <inheritdoc />
        public Task<ChaosPaged<TPage>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var allItems = ReadFile();
            var items = allItems.Skip((page - 1)*itemsPerPage).Take(itemsPerPage);

            return Task.FromResult(new ChaosPaged<TPage>
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = allItems.Count(),
                Items = items
            });

        }

        /// <inheritdoc />
        public Task<ChaosResult> UpdateAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var items = ReadFile().ToList();
            items.RemoveAll(x=>x.Id.Equals(page.Id));
            items.Add(page);
            WriteFile(items);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<TPage> FindByIdAsync(string pageId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var item = ReadFile().FirstOrDefault(x => x.Id.Equals(ConvertIdFromString(pageId)));
            return Task.FromResult(item);
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
        public Task<string> GetIdAsync(TPage page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            return Task.FromResult(ConvertIdToString(page.Id));
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
        
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
        }


        private Guid ConvertIdFromString(string pageId)
        {
            Guid id;
            if (!Guid.TryParse(pageId, out id))
            {
                id = Guid.Empty;
            }

            return id;
        }

        private string ConvertIdToString(Guid id)
        {
            if (Guid.Equals(id, default(Guid)))
            {
                return null;
            }
            return id.ToString();
        }

        private IEnumerable<TPage> ReadFile()
        {
            List<TPage> collection;
            lock (lockObject)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Options.StoreDirectoryName);
                var filename = Path.Combine(path, Options.PagesFileName);
                if (!File.Exists(filename))
                {
                    WriteFile(new List<TPage>());
                }
                collection = JsonConvert.DeserializeObject<List<TPage>>(File.ReadAllText(filename));
            }

            return collection;
        }

        private void WriteFile(List<TPage> objects)
        {
            lock (lockObject)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Options.StoreDirectoryName);
                var filename = Path.Combine(path, Options.PagesFileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.WriteAllText(filename, JsonConvert.SerializeObject(objects, Formatting.Indented));
            }
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
