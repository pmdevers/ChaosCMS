using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ChaosCMS.Json.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class JsonStore<TEntity> : IDisposable 
        where TEntity : class, IEntity
    {
        private bool isDisposed = false;
        private static object lockObject = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        protected JsonStore(IOptions<ChaosJsonStoreOptions> optionsAccessor)
        {
            this.Options = optionsAccessor?.Value ?? new ChaosJsonStoreOptions();
        }

        /// <summary>
        /// The <see cref="ChaosJsonStoreOptions"/> used to configure Chaos Json Store.
        /// </summary>
        protected internal ChaosJsonStoreOptions Options { get; }

        /// <inheritdoc />
        public Task<ChaosResult> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var items = ReadFile().ToList();
            items.Add(entity);
            WriteFile(items);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<ChaosResult> UpdateAsync(TEntity page, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var items = ReadFile().ToList();
            items.RemoveAll(x => x.Id.Equals(page.Id));
            items.Add(page);
            WriteFile(items);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<TEntity> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var item = ReadFile().FirstOrDefault(x => x.Id.Equals(ConvertIdFromString(id)));
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<string> GetIdAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Task.FromResult(ConvertIdToString(entity.Id));
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        protected Guid ConvertIdFromString(string pageId)
        {
            Guid id;
            if (!Guid.TryParse(pageId, out id))
            {
                id = Guid.Empty;
            }

            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string ConvertIdToString(Guid id)
        {
            if (Guid.Equals(id, default(Guid)))
            {
                return null;
            }
            return id.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<TEntity> ReadFile()
        {
            List<TEntity> collection;
            lock (lockObject)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Options.StoreDirectoryName);
                var filename = Path.Combine(path, typeof(TEntity).Name + Options.Extension);
                if (!File.Exists(filename))
                {
                    WriteFile(new List<TEntity>());
                }
                collection = JsonConvert.DeserializeObject<List<TEntity>>(File.ReadAllText(filename));
            }

            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        protected void WriteFile(List<TEntity> objects)
        {
            lock (lockObject)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), Options.StoreDirectoryName);
                var filename = Path.Combine(path, typeof(TEntity).Name + Options.Extension);
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
