﻿using ChaosCMS.LiteDB.Models;
using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChaosCMS.LiteDB.Stores
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LiteDBStore<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        private LiteDatabase database;
        private bool isDisposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public LiteDBStore(IOptions<ChaosLiteDBStoreOptions> optionsAccessor)
        {
            this.Options = optionsAccessor?.Value ?? new ChaosLiteDBStoreOptions();
            database = new LiteDatabase(this.Options.ConnectionString);
        }

        /// <summary>
        ///
        /// </summary>
        protected virtual internal LiteCollection<TEntity> Collection { get { return this.database.GetCollection<TEntity>(); } }
        
        /// <summary>
        /// The <see cref="ChaosJsonStoreOptions"/> used to configure Chaos Json Store.
        /// </summary>
        protected internal ChaosLiteDBStoreOptions Options { get; }

        /// <inheritdoc />
        public Task<ChaosResult> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            this.Collection.Insert(entity);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<ChaosResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            this.Collection.Update(entity);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<ChaosResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            this.Collection.Delete(entity.Id);
            return Task.FromResult(ChaosResult.Success);
        }

        /// <inheritdoc />
        public Task<ChaosPaged<TEntity>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var pages = this.Collection.Find(x => true, ((page - 1) * itemsPerPage), itemsPerPage);

            var paged = new ChaosPaged<TEntity>()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                Items = pages,
                TotalItems = this.Collection.Count()
            };

            return Task.FromResult(paged);
        }

        /// <inheritdoc />
        public Task<TEntity> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FindById(id);
            return Task.FromResult(item);
        }

        /// <inheritdoc />
        public Task<TEntity> FindByExternalIdAsync(string externalId, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var item = this.Collection.FindOne(x => x.ExternalId.Equals(externalId));
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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(disposing && !this.isDisposed)
            {
                this.isDisposed = true;
                database.Dispose();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected ObjectId ConvertIdFromString(string id)
        {
            var bsonValue = new ObjectId(id);
            if(bsonValue == null)
            {
                return ObjectId.NewObjectId();
            }
            return bsonValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string ConvertIdToString(ObjectId id)
        {
            if (Equals(id, ObjectId.NewObjectId()))
            {
                return null;
            }
            return id.ToString();
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
