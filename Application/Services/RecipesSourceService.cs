using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RecipesSourceService : IRecipesSourceService
    {
        private readonly IRecipesSource _recipesSource;
        private readonly ICache _cache;

        public RecipesSourceService(IRecipesSource recipesSource, ICache cache)
        {
            _recipesSource = recipesSource;
            _cache = cache;
        }

        public async Task<List<Recipe>> GetRecipesAsync(List<string> ingredients_en, CancellationToken ct)
        {
            return await _cache.GetOrCreateAsync(
                string.Join('_', ingredients_en.Order()),
                async () => await _recipesSource.GetRecipesAsync(ingredients_en, ct),
                ct
                );
        }
    }
}