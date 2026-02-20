using System.Text.Json.Serialization;

namespace Infrastructure.RecipesSource
{
    public class SpoonIngredient
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("original")]
        public string Original { get; set; } = "";

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; } = "";
    }
}
