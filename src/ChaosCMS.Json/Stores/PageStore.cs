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
        
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TPage> FindByUrlAsync(string urlPath, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var item = ReadFile().FirstOrDefault(x => x.Url.Equals(urlPath, StringComparison.CurrentCultureIgnoreCase));
            return Task.FromResult(item);
        }

        private List<TPage> ReadFile()
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
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
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
