using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CosmosDb.Models
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CategoryModel
    {
        public Guid Id { get; set; }

        public Guid CategoryId => Id;

        public Guid? ParentId { get; set; }

        public int SoftomiCategoryId { get; set; }

        public string? Name { get; set; }

        public string? CategoryHierachy { get; set; }

        public int Version { get; set; }

        public string? Type { get; set; }
    }
}
