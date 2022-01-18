namespace CosmosDb.Abstract
{
    public interface IGenericCosmosRepository<TEntity> : IAsyncDisposable
        where TEntity : class
    {
        string DatabaseId { get; }

        string ContainerId { get; }

        Task<TEntity> AddAsync(TEntity entity, string partitionKey);

        Task<TEntity?> GetItemAsync(string id);

        Task<TEntity> GetItemAsync(string id, string partitionKey);

        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetAllAsync(string partitionKey);

        Task UpdateAsync(TEntity entity, string partitionKey);

        Task DeleteAsync(string id, string partitionKey);
    }
}
