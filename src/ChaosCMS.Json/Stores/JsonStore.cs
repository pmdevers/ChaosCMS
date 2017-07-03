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
        private List<TEntity> collection;

        /// <summary>
        ///
        /// </summary>
        /// <param name="optionsAccessor"></param>
        protected JsonStore(IOptions<ChaosJsonStoreOptions> optionsAccessor)
        {
            this.Options = optionsAccessor?.Value ?? new ChaosJsonStoreOptions();
            this.collection  = this.ReadFile<TEntity>();
        }

        /// <summary>
        /// The <see cref="ChaosJsonStoreOptions"/> used to configure Chaos Json Store.
        /// </summary>
        protected internal ChaosJsonStoreOptions Options { get; }

        /// <summary>
        ///
        /// </summary>
        protected virtual internal IList<TEntity> Collection { get { return this.collection; } }

        /// <inheritdoc />
        public Task<ChaosResult> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            this.Collection.Add(entity);
            this.WriteFile(this.collection);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<ChaosResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            this.Collection.Remove(entity);
            this.Collection.Add(entity);
            this.WriteFile(this.collection);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<ChaosResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            this.Collection.Remove(entity);
            this.WriteFile(this.collection);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<TEntity> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FirstOrDefault(x => x.Id.Equals(this.ConvertIdFromString(id)));
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<TEntity> FindByExternalIdAsync(string externalId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FirstOrDefault(x => x.ExternalId != null && x.ExternalId.Equals(externalId));
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<string> GetIdAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Task.FromResult(this.ConvertIdToString(entity.Id));
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            this.isDisposed = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        protected Guid ConvertIdFromString(string pageId)
        {
            if (!Guid.TryParse(pageId, out Guid id))
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
        protected List<T> ReadFile<T>()
        {
            
                lock (lockObject)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), this.Options.StoreDirectoryName);
                    var filename = Path.Combine(path, typeof(T).Name + this.Options.Extension);
                    if (!File.Exists(filename))
                    {
                        this.WriteFile<T>(new List<T>());
                    }
                    var fileContents = File.ReadAllText(filename);
                    return JsonConvert.DeserializeObject<List<T>>(fileContents);
                }
            
        }

        /// <summary>
        ///
        /// </summary>
        protected void WriteFile<T>(List<T> collection)
        {
            if (collection != null)
            {
                lock (lockObject)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), this.Options.StoreDirectoryName);
                    var filename = Path.Combine(path, typeof(T).Name + this.Options.Extension);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllText(filename, JsonConvert.SerializeObject(collection, Formatting.Indented));
                }
            }
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }
    }
}