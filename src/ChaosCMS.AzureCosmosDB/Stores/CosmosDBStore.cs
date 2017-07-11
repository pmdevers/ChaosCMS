using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Azure.Documents;
using ChaosCMS.AzureCosmosDB.Models;
using Microsoft.Azure.Documents.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace ChaosCMS.AzureCosmosDB.Stores
{
    public class CosmosDBStore<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        private bool isDisposed;
        public DocumentClient client;
        private string CollectionId = typeof(TEntity).Name;

        public CosmosDBStore(IOptions<CosmosDBOptions> optionsAccessor)
        {
            this.Options = optionsAccessor.Value ?? new CosmosDBOptions();

            client = new DocumentClient(new Uri(this.Options.EndPoint), this.Options.Key);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public CosmosDBOptions Options { get; set; }

        public async Task<ChaosResult> CreateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var results = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.Options.DatabaseId, this.CollectionId), entity);
            return ChaosResult.Success;
        }

        public async Task<ChaosResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var results = await client.UpsertDocumentAsync(UriFactory.CreateDocumentUri(this.Options.DatabaseId, this.CollectionId, entity.Id), entity);
            return ChaosResult.Success;
        }

        public async Task<ChaosResult> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(this.Options.DatabaseId, this.CollectionId, entity.Id));

            return ChaosResult.Success;
        }

        public Task<ChaosPaged<TEntity>> FindPagedAsync(int page, int itemsPerPage, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();

            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();

            var query = this.client.CreateDocumentQuery<TEntity>(UriFactory.CreateDocumentCollectionUri(this.Options.DatabaseId, this.CollectionId));
        }

        public async Task<TEntity> FindByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            Document result = await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(this.Options.DatabaseId, this.CollectionId, id));
            return (TEntity)(dynamic)result;
        }

        public async Task<TEntity> FindByOriginAsync(string origin, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var query = this.client.CreateDocumentQuery<TEntity>(UriFactory.CreateDocumentCollectionUri(this.Options.DatabaseId, this.CollectionId), new FeedOptions() { MaxItemCount = 1 })
                .Where(x=>x.Origin == origin).AsDocumentQuery();

            var result = await query.ExecuteNextAsync<TEntity>();
            return result.FirstOrDefault();
        }

        public Task<string> GetIdAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.FromResult(entity.Id);
        }

        public Task SetOriginAsync(TEntity entity, string origin, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.Origin = origin;
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if(!this.isDisposed  && disposing)
            {
                this.isDisposed = true;
                this.client.Dispose();
            }
        }

        protected async Task<IEnumerable<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            this.ThrowIfDisposed();
            var query = this.client.CreateDocumentQuery<TEntity>(UriFactory.CreateDocumentCollectionUri(this.Options.DatabaseId, this.CollectionId), new FeedOptions() { MaxItemCount = 1 })
                .Where(predicate).AsDocumentQuery();

            List<TEntity> results = new List<TEntity>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<TEntity>());
            }

            return results;
        }

        protected void ThrowIfDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(this.Options.DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = this.Options.DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(this.Options.DatabaseId, this.CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(this.Options.DatabaseId),
                        new DocumentCollection { Id = this.CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }

}