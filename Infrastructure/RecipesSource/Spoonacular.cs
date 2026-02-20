using Application.Interfaces.RepositoryInterfaces;
using Domain.Entities;
using System.Net.Http.Json;

namespace Infrastructure.RecipesSource
{
    public class Spoonacular : IRecipesSource
    {
        private readonly HttpClient _httpClient;

        public Spoonacular(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("SPOONACULAR_API_URL") ?? throw new Exception("Spoonacular API address is not set."));
            _httpClient.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY") ?? throw new Exception("Spoonacular API key is not set."));
        }

        public async Task<List<Recipe>> GetRecipesAsync(List<string> ingredients_en, CancellationToken ct = default)
        {
            var ingredientsQuery = string.Join(",+", ingredients_en);

            var requestUrl = $"recipes/findByIngredients?ingredients={ingredientsQuery}&" +
                $"number=4&" +
                $"ranking=2&" +
                $"ignorePantry=true";
            var spoonacularRecipies = await _httpClient.GetFromJsonAsync<List<SpoonacularRecipe>>(requestUrl, ct) ?? [];
            return spoonacularRecipies.ToRecipes();
        }
    }
}