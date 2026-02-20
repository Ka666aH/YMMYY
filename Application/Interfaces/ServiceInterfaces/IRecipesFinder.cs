using Domain.Entities;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IRecipesFinder
    {
        Task<List<Recipe>> FindRecipesAsync (List<string> ingredients, string sourceLanguage, CancellationToken ct);
    }
}