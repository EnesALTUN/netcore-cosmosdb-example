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
