using Microsoft.Azure.Cosmos;
using CosmosDb.Abstract;
using CosmosDb.Models;

namespace CosmosDb.Concrete
{
    public class CosmosCategoryRepository : GenericCosmosRepository<CategoryModel>, ICosmosCategoryRepository
    {
        public override string DatabaseId => "modalog-db";

        public override string ContainerId => "categories";

        public readonly CosmosClient _cosmosClient;
        public readonly Container _container;

        public CosmosCategoryRepository(CosmosClient cosmosClient)
            : base(cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
        }
    }
}
