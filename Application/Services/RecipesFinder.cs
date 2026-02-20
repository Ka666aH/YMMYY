using Application.Interfaces.ServiceInterfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RecipesFinder : IRecipesFinder
    {
        private readonly ITranslatorService _ts;
        private readonly IRecipesSourceService _rss;

        public RecipesFinder(ITranslatorService ts, IRecipesSourceService rss)
        {
            _ts = ts;
            _rss = rss;
        }

        public async Task<List<Recipe>> FindRecipesAsync(List<string> ingredients, string sourceLanguage, CancellationToken ct)
        {
            if (ingredients.Count < 1) throw new ArgumentException("No ingredients.");

            List<string> ingredients_en = await _ts.TranslateIngredientsAsync(ingredients, sourceLanguage, ct);
            List<Recipe> recipes_en = await _rss.GetRecipesAsync(ingredients_en, ct);
            List<Recipe> recipes_sourceLanguage = await _ts.TranslateRecipeAsync(recipes_en, sourceLanguage, ct);
            return recipes_sourceLanguage;
        }
    }
}
