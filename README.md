# Used Technologies
- .Net 6.0
- Microsoft Azure Cosmos 3.23.0

# Service Definition
```
services.AddScoped<ICosmosCategoryRepository>(x => new CosmosCategoryRepository(new CosmosClient(hostcontext.Configuration.GetConnectionString("<ConnectionKey>"), new CosmosClientOptions
{
    SerializerOptions = new CosmosSerializationOptions
    {
        Indented = true
    }
})));
```
