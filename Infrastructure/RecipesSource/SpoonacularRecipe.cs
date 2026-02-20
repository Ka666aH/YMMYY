using System.Text.Json.Serialization;

namespace Infrastructure.RecipesSource
{
    public class SpoonacularRecipe
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("image")]
        public string Image { get; set; } = "";

        [JsonPropertyName("usedIngredients")]
        public List<SpoonIngredient> UsedIngredients { get; set; } = new();

        [JsonPropertyName("missedIngredients")]
        public List<SpoonIngredient> MissedIngredients { get; set; } = new();
    }
}
