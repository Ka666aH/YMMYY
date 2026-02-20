using Domain.Entities;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IRecipesSource
    {
        Task<List<Recipe>> GetRecipesAsync(List<string> ingredients_en, CancellationToken ct = default);
    }
}
