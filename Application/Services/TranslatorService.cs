using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Entities;
using Domain.Constants;


namespace Application.Services
{
    public class TranslatorService : ITranslatorService
    {
        private readonly ITranslator _translator;
        private readonly ICache _cache;

        public TranslatorService(ICache cache, ITranslator translator)
        {
            _cache = cache;
            _translator = translator;
        }

        public async Task<List<string>> TranslateIngredientsAsync(List<string> ingredients, string source, CancellationToken ct = default)
        {
            var tasks = ingredients.Select(ing =>
                _cache.GetOrCreateAsync(
                    ing,
                    async () => await _translator.TranslateAsync(ing, source, Languages.English, ct),
                    ct
                )
            );
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
        public async Task<Recipe> TranslateRecipeAsync(Recipe recipe, string target, CancellationToken ct = default)
        {
            var titleTask = _cache.GetOrCreateAsync(
                $"{recipe.Title}:{target}",
                async () => await _translator.TranslateAsync(recipe.Title, Languages.English, target, ct),
                ct);

            List<Task<string>> usedTasks = [];
            if (recipe.UsedIngredients.Count > 0)
            {

                usedTasks.AddRange(
                    recipe.UsedIngredients.Select(ing =>
                    _cache.GetOrCreateAsync(
                        $"{ing}:{target}",
                        async () => await _translator.TranslateAsync(ing, Languages.English, target, ct),
                        ct)
                    )
                    );
            }
            List<Task<string>> missingTasks = [];
            if (recipe.MissingIngredients.Count > 0)
            {
                missingTasks.AddRange(
                    recipe.MissingIngredients.Select(ing =>
                    _cache.GetOrCreateAsync(
                        $"{ing}:{target}",
                        async () => await _translator.TranslateAsync(ing, Languages.English, target, ct),
                        ct)
                    )
                    );
            }

            string title = await titleTask;
            var used = await Task.WhenAll(usedTasks);
            var missing = await Task.WhenAll(missingTasks);

            return new Recipe(title, recipe.ImageURL, used.ToList(), missing.ToList());
        }
    }
}