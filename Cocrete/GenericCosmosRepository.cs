using Microsoft.Azure.Cosmos;
using CosmosDb.Abstract;
using System.Reflection;

namespace CosmosDb.Concrete
{
    public abstract class GenericCosmosRepository<TEntity> : IGenericCosmosRepository<TEntity>, IAsyncDisposable
        where TEntity : class
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public abstract string DatabaseId { get; }
        public abstract string ContainerId { get; }

        public GenericCosmosRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
        }

        public async Task<TEntity> AddAsync(TEntity entity, string partitionKey)
        {
            var itemResponse = await _container.CreateItemAsync(entity, new PartitionKey(partitionKey));

            return itemResponse;
        }

        public async Task<TEntity?> GetItemAsync(string id)
        {
            TEntity? result = null;

            QueryDefinition queryString = new QueryDefinition($"SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", id);

            var query = _container.GetItemQueryIterator<TEntity>(queryString);

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                result = response.FirstOrDefault();
            }

            return result;
        }

        public async Task<TEntity> GetItemAsync(string id, string partitionKey)
        {
            ItemResponse<TEntity> itemResponse = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(partitionKey));

            return itemResponse.Resource;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var queryString = $"SELECT * FROM c";

            var query = _container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryString));

            List<TEntity> results = new();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<List<TEntity>> GetAllAsync(string partitionKey)
        {
            var queryString = $"SELECT * FROM c";

            QueryRequestOptions requestOptions = new() { PartitionKey = new PartitionKey(partitionKey) };

            var query = _container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryString), null, requestOptions);

            List<TEntity> results = new();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(TEntity entity, string partitionKey)
        {
            PropertyInfo? entityId = entity.GetType().GetProperty("Id");

            await _container.ReplaceItemAsync(entity, entityId?.ToString(), new PartitionKey(partitionKey));
        }

        public async Task DeleteAsync(string id, string partitionKey)
        {
            await _container.DeleteItemAsync<TEntity>(id, new PartitionKey(partitionKey));
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Run(() => _cosmosClient.Dispose());
        }
    }
}
